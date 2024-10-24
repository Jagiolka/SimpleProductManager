using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

public static class ProductCategoryExtensions
{
    public static ProductCategoryModel MapToProductCategoryModel(this ProductCategory productCategory) 
    {
        return new ProductCategoryModel(
            productCategory.Id, 
            productCategory.Name
            );
    }
}
