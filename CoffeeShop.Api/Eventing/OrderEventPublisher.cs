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
      var orderCreated = new OrderCreated { Id = order.Id, CustomerName = order.CustomerName };
      orderCreated.Coffees = order.Coffees.Select(coffee => new Coffee {
        Id = coffee.Id,
        Type = coffee.Type,
        NumberOfSugars = coffee.NumberOfSugars,
        NumberOfCreamers = coffee.NumberOfCreamers
      });
      
      orderCreated.Bagels = order.Bagels.Select(bagel => new Bagel {
        Id = bagel.Id,
        Type = bagel.Type,
        HasCreamCheese = bagel.HasCreamCheese,
        HasLox = bagel.HasLox
      });
      
      _endpoint.Publish<IOrderRequestedEvent>(orderCreated);
    }

    private class OrderCreated : IOrderRequestedEvent
    {
      public Guid Id { get; set; }

      public string CustomerName { get; set; }

      public IEnumerable<ICoffee> Coffees { get; set; }

      public IEnumerable<IBagel> Bagels { get; set; }
    }

    private class Coffee : ICoffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }

    private class Bagel : IBagel
    {
      public Guid Id { get; set; }

      public string Type { get; set; }

      public bool HasCreamCheese { get; set; }

      public bool HasLox { get; set; }
    }
  }
}