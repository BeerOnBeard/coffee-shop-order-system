using System;
using System.Collections.Generic;

namespace CoffeeShop.EventContracts
{
  public interface IOrderCreatedEvent
  {
    Guid Id { get; }
    string CustomerName { get; }
    IEnumerable<ICoffee> Coffees { get; }
  }
}
