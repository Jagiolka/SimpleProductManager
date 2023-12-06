namespace SimpleProductManager.DataLayer.DataModel;

public class SimpleProductModel
{
    public SimpleProductModel(Guid id, string name, List<ProductCategoryModel> ProductCategories)
    {
        this.Id = id;
        this.Name = name;
        this.ProductCategories = ProductCategories;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<ProductCategoryModel> ProductCategories { get; set; }
}