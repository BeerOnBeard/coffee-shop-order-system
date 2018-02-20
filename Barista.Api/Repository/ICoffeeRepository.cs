using System;
using System.Collections.Generic;
using Barista.Api.Models;

namespace Barista.Api.Repository
{
  public interface ICoffeeRepository
  {
    void AddCoffeeOrder(Coffee coffee);
    IEnumerable<Coffee> Get();
    void Update(Coffee coffee);
  }
}