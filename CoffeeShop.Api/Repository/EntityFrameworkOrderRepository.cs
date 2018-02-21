using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Api.Repository
{
  public class EntityFrameworkOrderRepository : IOrderRepository
  {
    private readonly OrderContext _context;

    public EntityFrameworkOrderRepository(OrderContext context)
    {
      _context = context;
    }

    public IEnumerable<Order> Get()
    {
      return _context.Orders
        .Include(order => order.Coffees)
        .Include(order => order.Bagels)
        .AsEnumerable();
    }
  }
}