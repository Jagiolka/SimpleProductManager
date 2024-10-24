namespace SimpleProductServices.Model;

public class ProductCategoryModel(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
}
