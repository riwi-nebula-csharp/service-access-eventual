namespace service_access_eventual.Models;


public class Performance
{
    public int Id { get; set; }
    public int PlayId { get; set; }
    public DateOnly PerformanceDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public decimal TicketPrice { get; set; }
    public DateTime SalesStartDate { get; set; }
    public DateTime SalesEndDate { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Nav
    public Play Play { get; set; } = null!;
}