using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<IEnumerable<Order>> GetUnfulfilled()
    {
      return await _context.Orders
        .Include(order => order.Coffees)
        .Include(order => order.Bagels)
        .Where(order => !order.IsFulfilled)
        .ToListAsync();
    }
  }
}