using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Repository
{
  public interface IOrderRepository
  {
    Task<IEnumerable<Order>> GetUnfulfilled();
  }
}