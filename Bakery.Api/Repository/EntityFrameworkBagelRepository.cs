using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Api.Repository
{
  public class EntityFrameworkBagelRepository : IBagelRepository
  {
    private readonly BagelContext _context;

    public EntityFrameworkBagelRepository(BagelContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Bagel>> GetIncomplete()
    {
      return await _context.Bagels.Where(b => !b.IsComplete).ToListAsync();
    }
  }
}