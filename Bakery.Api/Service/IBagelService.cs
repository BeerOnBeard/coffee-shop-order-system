using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery.Api.Models;

namespace Bakery.Api.Service
{
  public interface IBagelService
  {
    Task<IEnumerable<Bagel>> Get();
    Task Complete(Bagel bagel);
  }
}