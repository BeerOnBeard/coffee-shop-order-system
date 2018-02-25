using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barista.Api.Models;

namespace Barista.Api.Service
{
  public interface ICoffeeService
  {
    Task<IEnumerable<Coffee>> Get();
    
    Task Complete(Coffee coffee);
  }
}