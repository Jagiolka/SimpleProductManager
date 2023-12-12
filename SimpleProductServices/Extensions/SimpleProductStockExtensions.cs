namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;

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
