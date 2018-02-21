using System.Collections.Generic;
using Bakery.Api.Models;

namespace Bakery.Api.Repository
{
  public interface IBagelRepository
  {
    IEnumerable<Bagel> Get();
  }
}