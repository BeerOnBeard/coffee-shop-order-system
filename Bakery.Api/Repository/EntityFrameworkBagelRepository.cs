using System.Collections.Generic;
using System.Linq;
using Bakery.Api.Models;

namespace Bakery.Api.Repository
{
  public class EntityFrameworkBagelRepository : IBagelRepository
  {
    private readonly BagelContext _context;

    public EntityFrameworkBagelRepository(BagelContext context)
    {
      _context = context;
    }
    public IEnumerable<Bagel> Get()
    {
      return _context.Bagels.AsEnumerable();
    }
  }
}