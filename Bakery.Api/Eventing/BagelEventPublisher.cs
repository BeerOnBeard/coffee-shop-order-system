using System;
using Bakery.Api.Models;
using Bakery.EventContracts;
using MassTransit;

namespace Bakery.Api.Eventing
{
  public class BagelEventPublisher : IBagelEventPublisher
  {
    private readonly IPublishEndpoint _endpoint;
    public BagelEventPublisher(IPublishEndpoint endpoint)
    {
      _endpoint = endpoint;
    }
    public void PublishCompleted(Bagel bagel)
    {
      var bagelCompleted = new BagelCompleted { Id = bagel.Id, OrderId = bagel.OrderId };
      _endpoint.Publish<IBagelCompletedEvent>(bagelCompleted);
    }

    private class BagelCompleted : IBagelCompletedEvent
    {
      public Guid Id { get; set; }
      public Guid OrderId { get; set; }
    }
  }
}