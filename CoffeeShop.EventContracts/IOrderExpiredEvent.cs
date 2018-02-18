using System;

namespace CoffeeShop.EventContracts
{
    public interface IOrderExpiredEvent
    {
      Guid OrderId { get; }
      string CustomerName { get; }
    }
}