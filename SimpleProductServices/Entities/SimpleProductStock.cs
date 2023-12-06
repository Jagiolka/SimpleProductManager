namespace SimpleProductServices.Entities;

public partial class SimpleProductStock
{
    public Guid SimpleProductId { get; set; }

    public int Quantity { get; set; }

    public virtual SimpleProduct SimpleProduct { get; set; } = null!;
}
