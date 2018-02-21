using System.Collections.Generic;

namespace CoffeeShop.Api.Models
{
  public class CreateOrder
  {
    public string CustomerName { get; set; }
    public IEnumerable<Coffee> Coffees { get; set; }
    public IEnumerable<Bagel> Bagels { get; set; }

    public class Coffee
    {
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }

    public class Bagel
    {
      public string Type { get; set; }
      public bool HasCreamCheese { get; set; }
      public bool HasLox { get; set; }
    }
  }
}