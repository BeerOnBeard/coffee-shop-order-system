using System;
using System.Collections.Generic;

namespace CoffeeShop.EventContracts
{
  public interface IOrderRequestedEvent
  {
    Guid Id { get; }
    string CustomerName { get; }
    IEnumerable<ICoffee> Coffees { get; }
    IEnumerable<IBagel> Bagels { get; }
  }
}
