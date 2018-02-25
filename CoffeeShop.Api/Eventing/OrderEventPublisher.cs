using System;
using System.Collections.Generic;
using System.Linq;
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

    public void PublishRequested(Order order)
    {
      var orderCreated = new OrderRequestedEvent { Id = order.Id, CustomerName = order.CustomerName };
      orderCreated.Coffees = order.Coffees.Select(coffee => new OrderRequestedEvent.Coffee {
        Id = coffee.Id,
        Type = coffee.Type,
        NumberOfSugars = coffee.NumberOfSugars,
        NumberOfCreamers = coffee.NumberOfCreamers
      });
      
      orderCreated.Bagels = order.Bagels.Select(bagel => new OrderRequestedEvent.Bagel {
        Id = bagel.Id,
        Type = bagel.Type,
        HasCreamCheese = bagel.HasCreamCheese,
        HasLox = bagel.HasLox
      });
      
      _endpoint.Publish<IOrderRequestedEvent>(orderCreated);
    }

    public void PublishFulfilled(Order order)
    {
      _endpoint.Publish<IOrderFulfilledEvent>(new { Id = order.Id });
    }
  }
}