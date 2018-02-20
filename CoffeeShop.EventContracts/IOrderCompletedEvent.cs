using System;

namespace CoffeeShop.EventContracts
{
  public interface IOrderCompletedEvent
  {
    Guid Id { get; }
  }
}