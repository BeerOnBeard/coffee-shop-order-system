using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Api.Models;

namespace CoffeeShop.Api.Repository
{
  public class InMemoryOrderRepository : IOrderRepository
  {
    private readonly OrderContext _context;

    public InMemoryOrderRepository(OrderContext context)
    {
      _context = context;
    }
    public void Create(Order order)
    {
      _context.Orders.Add(order);
      _context.SaveChanges();
    }

    public void Update(Order order)
    {
      _context.Orders.Update(order);
      _context.SaveChanges();
    }

    public IEnumerable<Order> Get()
    {
      return _context.Orders.AsEnumerable();
    }
  }
}