namespace SimpleProductServices.Entities;

public partial class ProductCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SimpleProduct> SimpleProducts { get; set; } = new List<SimpleProduct>();
}
