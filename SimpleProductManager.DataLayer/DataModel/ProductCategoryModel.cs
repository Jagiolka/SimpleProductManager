namespace SimpleProductManager.DataLayer.DataModel;

public class ProductCategoryModel
{
    public ProductCategoryModel(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
