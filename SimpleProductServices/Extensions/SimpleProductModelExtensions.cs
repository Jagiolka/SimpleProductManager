namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;
using System.Linq;

public static class SimpleProductModelExtensions
{
    public static SimpleProduct MapToSimpleProductModel(this SimpleProductModel simpleProductModel)
    {
        var productCategories = simpleProductModel.ProductCategories.Select(categoryModel => categoryModel.MapToProductCategory()).ToList();

        return new SimpleProduct()
        {
            Id = simpleProductModel.Id,
            Name = simpleProductModel.Name,
            ProductCategories = productCategories,
        };
    }
}
