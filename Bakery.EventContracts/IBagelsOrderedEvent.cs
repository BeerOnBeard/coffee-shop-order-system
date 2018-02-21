using System;
using System.Collections.Generic;

namespace Bakery.EventContracts
{
  public interface IBagelsOrderedEvent
  {
    Guid OrderId { get; }
    IEnumerable<IBagel> Bagels { get; }
  }
}