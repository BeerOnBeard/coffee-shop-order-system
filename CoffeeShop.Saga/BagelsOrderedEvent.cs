using System;
using System.Collections.Generic;
using Bakery.EventContracts;

namespace CoffeeShop.Saga
{
  public class BagelsOrderedEvent : IBagelsOrderedEvent
  {
    public Guid OrderId { get; set; }

    public IEnumerable<IBagel> Bagels { get; set; }

    public class Bagel : IBagel
    {
      public Guid Id { get; set; }

      public string Type { get; set; }

      public bool HasCreamCheese { get; set; }

      public bool HasLox { get; set; }
    }
  }
}