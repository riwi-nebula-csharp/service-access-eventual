namespace service_access_eventual.DTOs;

public class ScanSuccessDto
{
    public bool AccessGranted { get; set; } = true;
    public string? OwnerName { get; set; }
    public string OwnerEmail { get; set; } = null!;
    public string PlayName { get; set; } = null!;
    public string PerformanceDate { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public SeatLocationDto Seat { get; set; } = null!;
    public string ScannedAt { get; set; } = null!;
    public int ScannedBy { get; set; }
}

public class SeatLocationDto
{
    public string Row { get; set; } = null!;
    public int Number { get; set; }
    public int SeatOrder { get; set; }
}

public class ScanDeniedDto
{
    public bool AccessGranted { get; set; } = false;
    public string Reason { get; set; } = null!;
    public string? ScannedAt { get; set; }
}