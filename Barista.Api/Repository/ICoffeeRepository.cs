using System;
using System.Collections.Generic;
using Barista.Api.Models;

namespace Barista.Api.Repository
{
  public interface ICoffeeRepository
  {
    IEnumerable<Coffee> Get();
  }
}