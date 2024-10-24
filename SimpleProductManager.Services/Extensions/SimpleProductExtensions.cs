using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

using System.Linq;

public static class SimpleProductExtensions
{
    public static SimpleProductModel MapToSimpleProductModel(this SimpleProduct simpleProduct) 
    {
        var productCategories = simpleProduct.ProductCategories.Select(category => category.MapToProductCategoryModel()).ToList();

        return new SimpleProductModel(
            simpleProduct.Id,
            simpleProduct.Name,
            productCategories
            );
    }
}
