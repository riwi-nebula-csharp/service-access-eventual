namespace service_access_eventual.Models;

public class Ticket
{
    public int Id { get; set; }
    public Guid QrUuid { get; set; }
    public int PurchaseId { get; set; }
    public int PerformanceSeatId { get; set; }
    public int OwnerId { get; set; }
    public string OwnerEmail { get; set; } = null!;
    public decimal PriceAtPurchase { get; set; }
    public string Status { get; set; } = null!;
    public int? SoldBy { get; set; }
    public string? QrUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Nav
    public PerformanceSeat PerformanceSeat { get; set; } = null!;
}