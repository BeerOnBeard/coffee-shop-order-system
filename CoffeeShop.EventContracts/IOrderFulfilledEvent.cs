using System;

namespace CoffeeShop.EventContracts
{
  public interface IOrderFulfilledEvent
  {
    Guid Id { get; set; }
  }
}