using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

public static class SimpleProductStockExtensions
{
    public static SimpleProductStockModel MapToSimpleProductStockModel(this SimpleProductStock simpleProductStock) 
    {
        var simpleProduct = simpleProductStock.SimpleProduct;

        return new SimpleProductStockModel(
            simpleProduct.Id,
            simpleProduct.Name,
            simpleProduct.ProductCategories.Select(category => category.MapToProductCategoryModel()).ToList(),
            simpleProductStock.Quantity
            );
    }
}
