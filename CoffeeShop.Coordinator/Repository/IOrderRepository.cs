using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Coordinator.Repository
{
  public interface IOrderRepository
  {
    Task<Order> Get(Guid orderId);
    Task Create(Order order);
    Task Update(Order order);
  }
}