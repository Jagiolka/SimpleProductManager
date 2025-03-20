using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleProductManager.Services.Entities;

public class SimpleProduct
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public Guid SimpleProductCategoryId { get; set; }

    public required SimpleProductCategory SimpleProductCategory { get; set; }
}
