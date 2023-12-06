namespace SimpleProductServices.Extensions;

using SimpleProductManager.DataLayer.DataModel;
using SimpleProductServices.Entities;

public static class SimpleProductStockExtensions
{
    public static SimpleProductStockModel MapToSimpleProductStockModel(this SimpleProductStock simpleProductStock) 
    {
        return new SimpleProductStockModel(
            simpleProductStock.SimpleProduct.MapToSimpleProductModel(), 
            simpleProductStock.Quantity
            );
    }
}
