using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Service
{
  public interface IOrderService
  {
    Task<IEnumerable<Order>> Get();
    Task<Order> Create(CreateOrder order);
    Task MarkFulfilled(Guid id);
  }
}