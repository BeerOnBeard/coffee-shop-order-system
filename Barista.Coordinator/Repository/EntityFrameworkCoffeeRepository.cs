using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Barista.Coordinator.Repository
{
  public class EntityFrameworkCoffeeRepository : ICoffeeRepository
  {
    private readonly CoffeeContext _context;

    public EntityFrameworkCoffeeRepository(CoffeeContext context)
    {
      _context = context;
    }
    public async Task AddCoffeeOrder(Coffee coffee)
    {
      await _context.Coffees.AddAsync(coffee);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Coffee>> Get()
    {
      return await _context.Coffees.ToListAsync();
    }

    public async Task Update(Coffee coffee)
    {
      _context.Coffees.Update(coffee);
      await _context.SaveChangesAsync();
    }
  }
}