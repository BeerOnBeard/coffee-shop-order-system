using System;
using System.Collections.Generic;
using Barista.Api.Models;

namespace Barista.Api.Service
{
  public interface ICoffeeService
  {
    IEnumerable<Coffee> Get();
    
    void Complete(Coffee coffee);
  }
}