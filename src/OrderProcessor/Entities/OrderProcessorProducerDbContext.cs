using Microsoft.EntityFrameworkCore;

namespace OrderProcessor.Producer.Entities;

public class OrderProcessorProducerDbContext : DbContext
{
    public OrderProcessorProducerDbContext(DbContextOptions<OrderProcessorProducerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<TransportCompany> TransportCompanies => Set<TransportCompany>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.SenderAddress);

        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.ReceiverAddress);

        modelBuilder.Entity<TransportCompany>()
            .OwnsOne(t => t.HeadquartersAddress);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany();
    }
}


