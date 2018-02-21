using Bakery.Api.Models;

namespace Bakery.Api.Eventing
{
  public interface IBagelEventPublisher
  {
    void PublishCompleted(Bagel bagel);
  }
}