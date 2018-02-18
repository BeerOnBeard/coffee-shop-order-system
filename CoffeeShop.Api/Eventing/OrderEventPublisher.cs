using System;
using CoffeeShop.Api.Models;
using CoffeeShop.EventContracts;
using MassTransit;

namespace CoffeeShop.Api.Eventing
{
  public class OrderEventPublisher : IOrderEventPublisher
  {
    private readonly IPublishEndpoint _endpoint;

    public OrderEventPublisher(IPublishEndpoint endpoint)
    {
      _endpoint = endpoint;
    }

    public void PublishCreated(Order order)
    {
      _endpoint.Publish<IOrderCreatedEvent>(new OrderCreated { Id = order.Id });
    }

    private class OrderCreated : IOrderCreatedEvent
    {
      public Guid Id { get; set; }
    }
  }
}