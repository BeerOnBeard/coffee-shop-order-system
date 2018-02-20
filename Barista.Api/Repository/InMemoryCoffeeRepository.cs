using System.Collections.Generic;
using System.Linq;
using Barista.Api.Models;

namespace Barista.Api.Repository
{
  public class InMemoryCoffeeRepository : ICoffeeRepository
  {
    private readonly CoffeeContext _context;

    public InMemoryCoffeeRepository(CoffeeContext context)
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