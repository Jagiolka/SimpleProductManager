namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;
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
            SimpleProduct = productModel,
            Quantity = simpleProductStockModel.Quantity,
        };
    }
}
