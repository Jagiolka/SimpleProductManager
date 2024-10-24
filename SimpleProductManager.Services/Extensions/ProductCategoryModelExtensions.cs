using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

public static class ProductCategoryModelExtensions
{
    public static ProductCategory MapToProductCategory(this ProductCategoryModel productCategory) 
    {
        return new ProductCategory()
        {
            Id = productCategory.Id,
            Name = productCategory.Name,
        };
    }
}
