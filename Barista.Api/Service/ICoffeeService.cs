using System.Collections.Generic;
using Barista.Api.Models;

namespace Barista.Api.Service
{
  public interface ICoffeeService
  {
    void AddCoffeeOrder(Coffee coffee);

    IEnumerable<Coffee> Get();
  }
}