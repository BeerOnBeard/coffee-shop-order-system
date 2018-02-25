using System.Threading.Tasks;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Eventing
{
  public interface IOrderEventPublisher
  {
    Task PublishRequested(Order order);
    Task PublishFulfilled(Order order);
  }
}