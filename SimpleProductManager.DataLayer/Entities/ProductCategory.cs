using System.ComponentModel.DataAnnotations;

namespace SimpleProductManager.Data.Entities;

public partial class ProductCategory
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SimpleProduct> SimpleProducts { get; set; } = new List<SimpleProduct>();
}
