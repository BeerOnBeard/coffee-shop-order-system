using System.Threading.Tasks;
using Barista.Api.Models;

namespace Barista.Api.Eventing
{
  public interface ICoffeeEventPublisher
  {
    Task PublishCompleted(Coffee coffee);
  }
}