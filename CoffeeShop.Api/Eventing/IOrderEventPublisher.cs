using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Eventing
{
  public interface IOrderEventPublisher
  {
    void PublishCreated(Order order);
  }
}