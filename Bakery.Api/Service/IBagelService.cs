using System.Collections.Generic;
using Bakery.Api.Models;

namespace Bakery.Api.Service
{
  public interface IBagelService
  {
    IEnumerable<Bagel> Get();
    void Complete(Bagel bagel);
  }
}