namespace SimpleProductServices.Model;

public class SimpleProductModel(Guid id, string name, string description, decimal price, SimpleProductCategoryModel simpleProductCategory)
{
    public Guid Id { get; init; } = id;
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public SimpleProductCategoryModel SimpleProductCategory { get; init; } = simpleProductCategory;
    public decimal Price { get; init; } = price;
}