using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Api.Eventing;
using CoffeeShop.Api.Models;
using CoffeeShop.Api.Repository;

namespace CoffeeShop.Api.Service
{
  public class OrderService : IOrderService
  {
    private readonly IOrderRepository _repository;
    private readonly IOrderEventPublisher _publisher;

    public OrderService(IOrderRepository repository, IOrderEventPublisher publisher)
    {
      _publisher = publisher;
      _repository = repository;
    }

    public Order Create(CreateOrder order)
    {
      var newOrder = new Order
      {
        Id = Guid.NewGuid(),
        CustomerName = order.CustomerName,
        Coffees = order.Coffees.Select(coffee => new Order.Coffee {
          Id = Guid.NewGuid(),
          Type = coffee.Type,
          NumberOfSugars = coffee.NumberOfSugars,
          NumberOfCreamers = coffee.NumberOfCreamers
        }).ToList(),
        Bagels = order.Bagels.Select(bagel => new Order.Bagel {
          Id = Guid.NewGuid(),
          Type = bagel.Type,
          HasCreamCheese = bagel.HasCreamCheese,
          HasLox = bagel.HasLox
        }).ToList()
      };

      _publisher.PublishRequested(newOrder);
      return newOrder;
    }

    public IEnumerable<Order> Get()
    {
      return _repository.Get();
    }
  }
}