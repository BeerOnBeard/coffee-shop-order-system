using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Eventing
{
  public interface IOrderEventPublisher
  {
    void PublishRequested(Order order);
  }
}