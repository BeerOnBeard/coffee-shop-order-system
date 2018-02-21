using System;
using System.Collections.Generic;

namespace CoffeeShop.Api.Models
{
  public class Order
  {
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public bool IsComplete { get; set; }
    public ICollection<Coffee> Coffees { get; set; }
    public ICollection<Bagel> Bagels { get; set; }

    public class Coffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }

    public class Bagel
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public bool HasCreamCheese { get; set; }
      public bool HasLox { get; set; }
    }
  }
}