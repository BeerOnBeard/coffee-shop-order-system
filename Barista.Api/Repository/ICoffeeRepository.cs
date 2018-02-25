using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barista.Api.Models;

namespace Barista.Api.Repository
{
  public interface ICoffeeRepository
  {
    Task<IEnumerable<Coffee>> GetIncomplete();
  }
}