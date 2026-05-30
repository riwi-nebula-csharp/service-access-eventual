using System.ComponentModel.DataAnnotations;

namespace service_access_eventual.DTOs;

public class ScanRequestDto
{
    [Required(ErrorMessage = "El qr_uuid es requerido.")]
    [MinLength(1, ErrorMessage = "El qr_uuid no puede estar vacío.")]
    public string QrUuid { get; set; } = null!;
}