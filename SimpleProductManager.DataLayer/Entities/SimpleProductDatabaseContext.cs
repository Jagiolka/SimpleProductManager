namespace SimpleProductManager.Data.Entities;

public partial class SimpleProductDatabaseContext(DbContextOptions<SimpleProductDatabaseContext> options) : DbContext(options)
{
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<SimpleProduct> SimpleProducts { get; set; }

    public virtual DbSet<SimpleProductStock> SimpleProductStocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SimpleProductDatabase;Trusted_Connection=True;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3213E83F27703032");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SimpleProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SimplePr__3213E83F9B49F9D3");

            entity.ToTable("SimpleProduct");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasMany(d => d.ProductCategories).WithMany(p => p.SimpleProducts)
                .UsingEntity<Dictionary<string, object>>(
                    "SimpleProductCategory",
                    r => r.HasOne<ProductCategory>().WithMany()
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SimplePro__produ__2B3F6F97"),
                    l => l.HasOne<SimpleProduct>().WithMany()
                        .HasForeignKey("SimpleProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SimplePro__simpl__2A4B4B5E"),
                    j =>
                    {
                        j.HasKey("SimpleProductId", "ProductCategoryId").HasName("PK__SimplePr__E3A8F966AF551869");
                        j.ToTable("SimpleProductCategory");
                        j.IndexerProperty<Guid>("SimpleProductId").HasColumnName("simple_product_id");
                        j.IndexerProperty<Guid>("ProductCategoryId").HasColumnName("product_category_id");
                    });
        });

        modelBuilder.Entity<SimpleProductStock>(entity =>
        {
            entity.HasKey(e => e.SimpleProductId);

            entity.ToTable("SimpleProductStock");

            entity.Property(e => e.SimpleProductId)
                .ValueGeneratedNever()
                .HasColumnName("simple_product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.SimpleProduct).WithOne(p => p.SimpleProductStock)
                .HasForeignKey<SimpleProductStock>(d => d.SimpleProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SimplePro__simpl__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
