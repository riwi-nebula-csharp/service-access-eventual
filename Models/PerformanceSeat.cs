namespace service_access_eventual.Models;

public class PerformanceSeat
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public int PerformanceId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? ReservedUntil { get; set; }
    public int? ScannedBy { get; set; }
    public DateTime? ScannedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Nav
    public Seat Seat { get; set; } = null!;
    public Performance Performance { get; set; } = null!;
}