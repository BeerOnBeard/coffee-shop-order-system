using Barista.Api.Models;

namespace Barista.Api.Eventing
{
  public interface ICoffeeEventPublisher
  {
    void PublishCompleted(Coffee coffee);
  }
}