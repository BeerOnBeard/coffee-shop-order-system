using System;

namespace CoffeeShop.EventContracts
{
  public interface IOrderCreatedEvent
  {
    Guid Id { get; }
  }
}
