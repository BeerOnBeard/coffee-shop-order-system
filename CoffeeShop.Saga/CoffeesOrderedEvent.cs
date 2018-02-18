using System;
using System.Collections.Generic;
using Barista.EventContracts;

namespace CoffeeShop.Saga
{
  public class CoffeesOrderedEvent : ICoffeesOrderedEvent
  {
    public Guid OrderId { get; set;}
    public IEnumerable<ICoffee> Coffees { get; set;}

    public class CoffeeOrder : ICoffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }
  }
}