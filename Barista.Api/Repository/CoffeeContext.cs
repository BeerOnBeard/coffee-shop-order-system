using Barista.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Barista.Api.Repository
{
  public class CoffeeContext : DbContext
  {
    public CoffeeContext(DbContextOptions<CoffeeContext> options)
      : base(options)
    {
    }

    public DbSet<Coffee> Coffees { get; set; }
  }
}