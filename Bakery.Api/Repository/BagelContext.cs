using Bakery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Api.Repository
{
  public class BagelContext : DbContext
  {
    public BagelContext(DbContextOptions<BagelContext> options)
      : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Bagel> Bagels { get; set; }
  }
}