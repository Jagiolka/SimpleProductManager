using System.ComponentModel.DataAnnotations;

namespace SimpleProductManager.Services.Entities;

public class SimpleProductCategory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(255)]
    public required string Name { get; set; }

    public ICollection<SimpleProduct> SimpleProducts { get; set; } = [];
}
