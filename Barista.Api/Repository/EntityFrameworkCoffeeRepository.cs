using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barista.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Barista.Api.Repository
{
  public class EntityFrameworkCoffeeRepository : ICoffeeRepository
  {
    private readonly CoffeeContext _context;

    public EntityFrameworkCoffeeRepository(CoffeeContext context)
    {
      _context = context;
    }
    
    public async Task<IEnumerable<Coffee>> GetIncomplete()
    {
      return await _context.Coffees.Where(c => !c.IsComplete).ToListAsync();
    }
  }
}