using OrderProcessor.Producer.Entities;

namespace OrderProcessor.Producer;

public class FuncOrders
{
    public async Task<string> PublishOrderEvent(Order order)
    {
        return "Order published";
    }
}
