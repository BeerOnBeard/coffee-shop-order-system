using System.Collections.Generic;
using System.Threading.Tasks;
using Bakery.Api.Models;

namespace Bakery.Api.Repository
{
  public interface IBagelRepository
  {
    Task<IEnumerable<Bagel>> GetIncomplete();
  }
}