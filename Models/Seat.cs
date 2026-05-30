namespace service_access_eventual.Models;

public class Seat
{
    public int Id { get; set; }
    public string RowName { get; set; } = null!;
    public int RowOrder { get; set; }
    public int Number { get; set; }
    public int SeatOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}