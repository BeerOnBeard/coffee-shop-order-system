using System.Collections.Generic;
using System.Linq;
using Barista.Api.Models;

namespace Barista.Api.Repository
{
  public class EntityFrameworkCoffeeRepository : ICoffeeRepository
  {
    private readonly CoffeeContext _context;

    public EntityFrameworkCoffeeRepository(CoffeeContext context)
    {
      _context = context;
    }
    
    public IEnumerable<Coffee> Get()
    {
      return _context.Coffees.AsEnumerable();
    }
  }
}