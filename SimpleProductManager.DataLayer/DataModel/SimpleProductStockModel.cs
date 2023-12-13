namespace SimpleProductManager.DataLayer.DataModel;

using System.Text;

public class SimpleProductStockModel
{
    public SimpleProductStockModel() : this(Guid.NewGuid(), string.Empty, new List<ProductCategoryModel>(), 0) // new product as default
    {
    }

    public SimpleProductStockModel(Guid simpleProductModelId, string productName, List<ProductCategoryModel> categories, int quantity)
    {
        this.SimpleProductModelId = simpleProductModelId;
        this.Name = productName;
        this.ProductCategories = categories;
        this.Quantity = quantity;
    }

    public Guid SimpleProductModelId { get; init; }
    public string Name { get; set; }
    public List<ProductCategoryModel> ProductCategories { get; set; }
    public int Quantity { get; init; }
}
