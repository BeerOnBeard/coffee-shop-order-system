using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Coordinator.Repository
{
  public class EntityFrameworkBagelRepository : IBagelRepository
  {
    private readonly BagelContext _context;

    public EntityFrameworkBagelRepository(BagelContext context)
    {
      _context = context;
    }
    public async Task Add(Bagel bagel)
    {
      await _context.Bagels.AddAsync(bagel);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Bagel>> Get()
    {
      return await _context.Bagels.ToListAsync();
    }

    public async Task Update(Bagel bagel)
    {
      _context.Bagels.Update(bagel);
      await _context.SaveChangesAsync();
    }
  }
}