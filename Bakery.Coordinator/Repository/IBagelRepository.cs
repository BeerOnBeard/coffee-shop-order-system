using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Coordinator.Repository
{
  public interface IBagelRepository
  {
    Task Add(Bagel bagel);
    Task<IEnumerable<Bagel>> Get();
    Task Update(Bagel bagel);
  }
}