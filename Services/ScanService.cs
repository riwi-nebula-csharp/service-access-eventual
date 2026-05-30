
using service_access_eventual.Data;
using service_access_eventual.DTOs;
using Microsoft.EntityFrameworkCore;

namespace service_access_eventual.Services;

public class ScanService : IScanService
{
    private readonly AppDbContext _db;

    public ScanService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(bool success, object data)> ProcessScanAsync(string qrUuid, int scannedByEmployeeId)
    {
        if (!Guid.TryParse(qrUuid, out var parsedUuid))
        {
            return (false, new ScanDeniedDto
            {
                Reason = "El formato del código QR no es válido."
            });
        }

        var ticket = await _db.Tickets
            .Include(t => t.PerformanceSeat)
            .ThenInclude(ps => ps.Seat)
            .Include(t => t.PerformanceSeat)
            .ThenInclude(ps => ps.Performance)
            .ThenInclude(p => p.Play)
            .Where(t => t.QrUuid == parsedUuid && t.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (ticket == null)
        {
            return (false, new ScanDeniedDto
            {
                Reason = "Boleta no encontrada. El código QR no es válido."
            });
        }

        if (ticket.Status == "cancelled")
        {
            return (false, new ScanDeniedDto
            {
                Reason = "Esta boleta ha sido cancelada y no otorga acceso."
            });
        }

        if (ticket.Status == "used" || ticket.PerformanceSeat.ScannedBy != null)
        {
            return (false, new ScanDeniedDto
            {
                Reason = "⚠️ ALERTA: Esta boleta ya fue escaneada. Posible intento de acceso duplicado.",
                ScannedAt = ticket.PerformanceSeat.ScannedAt?.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        var performance = ticket.PerformanceSeat.Performance;

        if (performance.Status == "finished")
        {
            return (false, new ScanDeniedDto
            {
                Reason = "La función para esta boleta ya ha finalizado."
            });
        }

        var now = DateTime.UtcNow;

        ticket.Status = "used";
        ticket.UpdatedAt = now;

        ticket.PerformanceSeat.Status = "occupied";
        ticket.PerformanceSeat.ScannedBy = scannedByEmployeeId;
        ticket.PerformanceSeat.ScannedAt = now;
        ticket.PerformanceSeat.UpdatedAt = now;

        await _db.SaveChangesAsync();

        var seat = ticket.PerformanceSeat.Seat;
        var play = performance.Play;

        return (true, new ScanSuccessDto
        {
            OwnerName = null,
            OwnerEmail = ticket.OwnerEmail,
            PlayName = play.Name,
            PerformanceDate = performance.PerformanceDate.ToString("yyyy-MM-dd"),
            StartTime = performance.StartTime.ToString("HH:mm"),
            Seat = new SeatLocationDto
            {
                Row = seat.RowName,
                Number = seat.Number,
                SeatOrder = seat.SeatOrder
            },
            ScannedAt = now.ToString("yyyy-MM-dd HH:mm:ss"),
            ScannedBy = scannedByEmployeeId
        });
    }
}