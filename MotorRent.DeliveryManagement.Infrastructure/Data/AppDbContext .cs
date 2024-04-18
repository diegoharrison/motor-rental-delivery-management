using Microsoft.EntityFrameworkCore;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.DeliveryManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Moto> Motos { get; set; }
    public DbSet<Deliveryman> Deliverymen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
