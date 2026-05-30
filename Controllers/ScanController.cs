using System.Security.Claims;
using service_access_eventual.DTOs;
using service_access_eventual.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace service_access_eventual.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ScanController : ControllerBase
{
    private readonly IScanService _scanService;

    public ScanController(IScanService scanService)
    {
        _scanService = scanService;
    }

    [HttpPost("scan")]
    public async Task<IActionResult> Scan([FromBody] ScanRequestDto request)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(new { success = false, message = "Datos inválidos.", errors = ModelState });

        var employeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? User.FindFirst("sub")?.Value;

        if (employeeIdClaim == null || !int.TryParse(employeeIdClaim, out var employeeId))
            return Unauthorized(new { success = false, message = "Token inválido." });

        var permissions = User.FindAll("permissions").Select(c => c.Value).ToList();
        if (!permissions.Contains("access"))
            return StatusCode(403, new { success = false, message = "No tienes permiso para usar el portal de acceso." });

        var (success, data) = await _scanService.ProcessScanAsync(request.QrUuid, employeeId);

        return Ok(new
        {
            success,
            message = success ? "Acceso permitido." : "Acceso denegado.",
            data
        });
    }
}