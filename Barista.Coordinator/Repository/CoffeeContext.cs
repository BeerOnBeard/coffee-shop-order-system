using Microsoft.EntityFrameworkCore;

namespace Barista.Coordinator.Repository
{
  public class CoffeeContext : DbContext
  {
    public CoffeeContext(DbContextOptions<CoffeeContext> options)
      : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Coffee> Coffees { get; set; }
  }
}