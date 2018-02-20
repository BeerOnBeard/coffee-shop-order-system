using CoffeeShop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Api.Repository
{
  public class OrderContext : DbContext
  {
    public OrderContext(DbContextOptions<OrderContext> options)
      : base(options)
    {
      Database.EnsureCreated();
    }
    
    public DbSet<Order> Orders { get; set; }
  }
}