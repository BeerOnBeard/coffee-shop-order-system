using System;

namespace CoffeeShop.Api.Models
{
  public class Order
  {
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public int NumberOfCoffees { get; set; }
    public int NumberOfBagels { get; set; }
    public bool IsComplete { get; set; }
  }
}