namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;

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
