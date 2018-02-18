using System;
using System.Collections.Generic;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Repository
{
  public interface IOrderRepository
  {
    IEnumerable<Order> Get();
    void Create(Order order);
    void Update(Order order);
  }
}