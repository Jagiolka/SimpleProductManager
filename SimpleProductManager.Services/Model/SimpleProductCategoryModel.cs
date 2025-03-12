namespace SimpleProductServices.Model;

public class SimpleProductCategoryModel(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
}
