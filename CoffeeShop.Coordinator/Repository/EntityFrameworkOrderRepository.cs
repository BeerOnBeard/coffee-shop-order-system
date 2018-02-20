using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Coordinator.Repository
{
  public class EntityFrameworkOrderRepository : IOrderRepository
  {
    private readonly OrderContext _context;

    public EntityFrameworkOrderRepository(OrderContext context)
    {
      _context = context;
    }

    public async Task Update(Order order)
    {
      _context.Orders.Update(order);
      await _context.SaveChangesAsync();
    }

    public async Task Create(Order order)
    {
      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();
    }

    public async Task<Order> Get(Guid orderId)
    {
      return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
    }
  }
}