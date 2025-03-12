using Microsoft.EntityFrameworkCore;
using SimpleProductManager.Services.Entities;

namespace SimpleProductManager.Services;

public class SimpleProductDatabaseContext : DbContext
{
    public DbSet<SimpleProduct> SimpleProducts { get; set; }

    public DbSet<SimpleProductCategory> SimpleProductCategories { get; set; }

    public SimpleProductDatabaseContext(DbContextOptions<SimpleProductDatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SimpleProduct>()
            .HasOne(p => p.SimpleProductCategory)
            .WithMany(c => c.SimpleProducts)
            .HasForeignKey(p => p.SimpleProductCategoryId);

        base.OnModelCreating(modelBuilder);
    }
}
