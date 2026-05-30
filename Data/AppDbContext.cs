

using service_access_eventual.Models;
using Microsoft.EntityFrameworkCore;

namespace service_access_eventual.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<PerformanceSeat> PerformanceSeats { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Play> Plays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(e =>
        {
            e.ToTable("tickets");
            e.HasKey(t => t.Id);
            e.Property(t => t.Id).HasColumnName("id");
            e.Property(t => t.QrUuid).HasColumnName("qr_uuid");
            e.Property(t => t.PurchaseId).HasColumnName("purchase_id");
            e.Property(t => t.PerformanceSeatId).HasColumnName("performance_seat_id");
            e.Property(t => t.OwnerId).HasColumnName("owner_id");
            e.Property(t => t.OwnerEmail).HasColumnName("owner_email");
            e.Property(t => t.PriceAtPurchase).HasColumnName("price_at_purchase");
            e.Property(t => t.Status).HasColumnName("status");
            e.Property(t => t.SoldBy).HasColumnName("sold_by");
            e.Property(t => t.QrUrl).HasColumnName("qr_url");
            e.Property(t => t.CreatedAt).HasColumnName("created_at");
            e.Property(t => t.UpdatedAt).HasColumnName("updated_at");
            e.Property(t => t.DeletedAt).HasColumnName("deleted_at");

            e.HasOne(t => t.PerformanceSeat)
             .WithMany()
             .HasForeignKey(t => t.PerformanceSeatId);
        });

        modelBuilder.Entity<PerformanceSeat>(e =>
        {
            e.ToTable("performance_seats");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.SeatId).HasColumnName("seat_id");
            e.Property(p => p.PerformanceId).HasColumnName("performance_id");
            e.Property(p => p.Status).HasColumnName("status");
            e.Property(p => p.ReservedUntil).HasColumnName("reserved_until");
            e.Property(p => p.ScannedBy).HasColumnName("scanned_by");
            e.Property(p => p.ScannedAt).HasColumnName("scanned_at");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
            e.Property(p => p.UpdatedAt).HasColumnName("updated_at");

            e.HasOne(p => p.Seat)
             .WithMany()
             .HasForeignKey(p => p.SeatId);

            e.HasOne(p => p.Performance)
             .WithMany()
             .HasForeignKey(p => p.PerformanceId);
        });

        modelBuilder.Entity<Seat>(e =>
        {
            e.ToTable("seats");
            e.HasKey(s => s.Id);
            e.Property(s => s.Id).HasColumnName("id");
            e.Property(s => s.RowName).HasColumnName("row_name");
            e.Property(s => s.RowOrder).HasColumnName("row_order");
            e.Property(s => s.Number).HasColumnName("number");
            e.Property(s => s.SeatOrder).HasColumnName("seat_order");
            e.Property(s => s.CreatedAt).HasColumnName("created_at");
            e.Property(s => s.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Performance>(e =>
        {
            e.ToTable("performances");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.PlayId).HasColumnName("play_id");
            e.Property(p => p.PerformanceDate).HasColumnName("performance_date");
            e.Property(p => p.StartTime).HasColumnName("start_time");
            e.Property(p => p.EndTime).HasColumnName("end_time");
            e.Property(p => p.TicketPrice).HasColumnName("ticket_price");
            e.Property(p => p.SalesStartDate).HasColumnName("sale_start_date");
            e.Property(p => p.SalesEndDate).HasColumnName("sale_end_date");
            e.Property(p => p.Status).HasColumnName("status");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
            e.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            e.Property(p => p.DeletedAt).HasColumnName("deleted_at");

            e.HasOne(p => p.Play)
             .WithMany()
             .HasForeignKey(p => p.PlayId);
        });

        modelBuilder.Entity<Play>(e =>
        {
            e.ToTable("plays");
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.Name).HasColumnName("name");
            e.Property(p => p.Description).HasColumnName("description");
            e.Property(p => p.PosterUrl).HasColumnName("poster_url");
            e.Property(p => p.CreatedAt).HasColumnName("created_at");
            e.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            e.Property(p => p.DeletedAt).HasColumnName("deleted_at");
        });
    }
}