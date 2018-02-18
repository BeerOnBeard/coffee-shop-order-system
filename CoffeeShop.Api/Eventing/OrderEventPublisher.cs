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

    public void PublishCreated(Order order)
    {
      var orderCreated = new OrderCreated { Id = order.Id, CustomerName = order.CustomerName };
      orderCreated.Coffees = order.Coffees.Select(coffee => new OrderCoffee {
        Id = coffee.Id,
        Type = coffee.Type,
        NumberOfSugars = coffee.NumberOfSugars,
        NumberOfCreamers = coffee.NumberOfCreamers
      });
      
      _endpoint.Publish<IOrderCreatedEvent>(orderCreated);
    }

    private class OrderCreated : IOrderCreatedEvent
    {
      public Guid Id { get; set; }

      public string CustomerName { get; set; }

      public IEnumerable<ICoffee> Coffees { get; set; }
    }

    private class OrderCoffee : ICoffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }
  }
}