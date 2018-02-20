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
    public void Create(Order order)
    {
      _context.Orders.Add(order);
      _context.SaveChanges();
    }

    public IEnumerable<Order> Get()
    {
      return _context.Orders.Include(order => order.Coffees).AsEnumerable();
    }
  }
}