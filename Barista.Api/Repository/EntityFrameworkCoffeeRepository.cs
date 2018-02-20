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
    public void AddCoffeeOrder(Coffee coffee)
    {
      _context.Coffees.Add(coffee);
      _context.SaveChanges();
    }

    public IEnumerable<Coffee> Get()
    {
      return _context.Coffees.AsEnumerable();
    }

    public void Update(Coffee coffee)
    {
      _context.Coffees.Update(coffee);
      _context.SaveChanges();
    }
  }
}