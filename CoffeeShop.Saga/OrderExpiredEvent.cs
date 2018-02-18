using System;
using CoffeeShop.EventContracts;

namespace CoffeeShop.Saga
{
  public class OrderExpiredEvent : IOrderExpiredEvent
  {
    private readonly Order _order;
    public OrderExpiredEvent(Order order)
    {
      _order = order;
    }
    public Guid OrderId => _order.CorrelationId;
    public string CustomerName => _order.CustomerName;
  }
}