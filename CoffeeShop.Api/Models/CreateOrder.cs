namespace CoffeeShop.Api.Models
{
  public class CreateOrder
  {
    public string CustomerName { get; set; }
    public int NumberOfCoffees { get; set; }
    public int NumberOfBagels { get; set; }
  }
}