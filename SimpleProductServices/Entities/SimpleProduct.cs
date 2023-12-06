namespace SimpleProductServices.Entities;

using System;
using System.Collections.Generic;

public partial class SimpleProduct
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual SimpleProductStock? SimpleProductStock { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
