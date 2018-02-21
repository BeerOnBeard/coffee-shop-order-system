using System;
using System.Collections.Generic;
using Bakery.EventContracts;
using Barista.EventContracts;

namespace CoffeeShop.Saga
{
  public class CoffeesOrderedEvent : ICoffeesOrderedEvent
  {
    public Guid OrderId { get; set;}
    public IEnumerable<ICoffee> Coffees { get; set;}

    public class Coffee : ICoffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }

    public class Bagel : IBagel
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public bool HasCreamCheese { get; set; }
      public bool HasLox { get; set; }
    }
  }
}