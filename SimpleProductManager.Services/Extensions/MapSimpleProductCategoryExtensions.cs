using SimpleProductManager.Services.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

public static class MapSimpleProductCategoryExtensions
{
    public static SimpleProductCategoryModel MapToCategoryModel(this SimpleProductCategory Category) 
    {
        return new SimpleProductCategoryModel(Category.Id, Category.Name);
    }

    public static SimpleProductCategory MapToCategory(this SimpleProductCategoryModel Category)
    {
        return new SimpleProductCategory()
        {
            Id = Category.Id,
            Name = Category.Name,
        };
    }
}
