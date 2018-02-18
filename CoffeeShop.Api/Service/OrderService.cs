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
        NumberOfBagels = order.NumberOfBagels,
        NumberOfCoffees = order.NumberOfCoffees,
        IsComplete = false
      };

      _repository.Create(newOrder);
      _publisher.PublishCreated(newOrder);
      return newOrder;
    }

    public Order Complete(Guid orderId)
    {
      var order = _repository.Get().FirstOrDefault(o => o.Id == orderId);
      if (order != null)
      {
        order.IsComplete = true;
        _repository.Update(order);
      }

      return order;
    }

    public IEnumerable<Order> Get()
    {
      return _repository.Get();
    }
  }
}