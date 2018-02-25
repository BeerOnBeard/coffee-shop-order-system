using System.Threading.Tasks;
using Bakery.Api.Models;

namespace Bakery.Api.Eventing
{
  public interface IBagelEventPublisher
  {
    Task PublishCompleted(Bagel bagel);
  }
}