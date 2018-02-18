using System;
using System.Collections.Generic;

namespace Barista.EventContracts
{
  public interface ICoffeesOrderedEvent
  {
    Guid OrderId { get; }
    IEnumerable<ICoffee> Coffees { get; }
  }
}
