namespace SimpleProductServices.Model;

public class SimpleProductModel(Guid id, string name, string description, decimal price, SimpleProductCategoryModel simpleProductCategory)
{
    public Guid Id { get; init; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public SimpleProductCategoryModel SimpleProductCategory { get; set; } = simpleProductCategory;
    public decimal Price { get; set; } = price;
}