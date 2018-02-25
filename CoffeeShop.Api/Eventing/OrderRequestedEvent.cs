using System;
using System.Collections.Generic;
using CoffeeShop.EventContracts;

namespace CoffeeShop.Api.Eventing
{
  public class OrderRequestedEvent : IOrderRequestedEvent
  {
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public IEnumerable<ICoffee> Coffees { get; set; }
    public IEnumerable<IBagel> Bagels { get; set; }

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