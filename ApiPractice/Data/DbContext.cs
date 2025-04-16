using Microsoft.EntityFrameworkCore;
using ApiPractice.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<Invoice>().ToTable("Invoices");

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Client)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.ClientId);
    }
}
