namespace SimpleProductManager.DataLayer.DataModel;

public class SimpleProductStockModel
{
    public SimpleProductStockModel(SimpleProductModel simpleProductModel, int quantity)
    {
        this.simpleProductModel = simpleProductModel;
        this.Quantity = quantity;
    }

    public SimpleProductModel simpleProductModel { get; init; }
    public int Quantity { get; init; }
}
