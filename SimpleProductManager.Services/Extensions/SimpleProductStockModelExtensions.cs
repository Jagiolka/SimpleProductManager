using SimpleProductManager.Data.Entities;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

using System.Linq;

public static class SimpleProductStockModelExtensions
{
    public static SimpleProductStock MapToSimpleProductStockModel(this SimpleProductStockModel simpleProductStockModel)
    {
        var productCategories = simpleProductStockModel.ProductCategories.Select(categoryModel => categoryModel.MapToProductCategory()).ToList();
        var productModel = new SimpleProduct()
        {
            Id = simpleProductStockModel.SimpleProductModelId,
            Name = simpleProductStockModel.Name,
            ProductCategories = productCategories,
        };

        return new SimpleProductStock()
        {
            SimpleProductId = simpleProductStockModel.SimpleProductModelId,
            SimpleProduct = productModel,
            Quantity = simpleProductStockModel.Quantity,
        };
    }
}
