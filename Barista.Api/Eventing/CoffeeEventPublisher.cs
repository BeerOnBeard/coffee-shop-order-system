using System;
using System.Threading.Tasks;
using Barista.Api.Models;
using Barista.EventContracts;
using MassTransit;

namespace Barista.Api.Eventing
{
  public class CoffeeEventPublisher : ICoffeeEventPublisher
  {
    private readonly IPublishEndpoint _endpoint;
    public CoffeeEventPublisher(IPublishEndpoint endpoint)
    {
      _endpoint = endpoint;
    }

    public async Task PublishCompleted(Coffee coffee)
    {
      var coffeeCompleted = new CoffeeCompleted { Id = coffee.Id, OrderId = coffee.OriginalOrderId };
      await _endpoint.Publish<ICoffeeCompletedEvent>(coffeeCompleted);
    }

    private class CoffeeCompleted : ICoffeeCompletedEvent
    {
      public Guid Id { get; set; }

      public Guid OrderId { get; set; }
    }
  }
}