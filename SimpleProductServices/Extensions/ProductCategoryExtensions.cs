namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;

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
