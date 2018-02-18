using System.Collections.Generic;

namespace CoffeeShop.Api.Models
{
  public class CreateOrder
  {
    public string CustomerName { get; set; }
    public IEnumerable<Coffee> Coffees { get; set; }

    public class Coffee
    {
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }
  }
}