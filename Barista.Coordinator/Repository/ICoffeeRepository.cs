using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barista.Coordinator.Repository
{
  public interface ICoffeeRepository
  {
    Task AddCoffeeOrder(Coffee coffee);
    Task<IEnumerable<Coffee>> Get();
    Task Update(Coffee coffee);
  }
}