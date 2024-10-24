using System.ComponentModel.DataAnnotations;

namespace SimpleProductManager.Data.Entities;

public partial class SimpleProduct
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual SimpleProductStock? SimpleProductStock { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
