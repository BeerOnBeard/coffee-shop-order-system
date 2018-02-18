using System;
using System.Collections.Generic;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Service
{
  public interface IOrderService
  {
    IEnumerable<Order> Get();
    Order Create(CreateOrder order);
  }
}