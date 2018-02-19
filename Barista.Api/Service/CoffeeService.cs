using System.Collections.Generic;
using Barista.Api.Models;
using Barista.Api.Repository;

namespace Barista.Api.Service
{
  public class CoffeeService : ICoffeeService
  {
    private readonly ICoffeeRepository _repository;
    public CoffeeService(ICoffeeRepository repository)
    {
      _repository = repository;
    }

    public void AddCoffeeOrder(Coffee coffee)
    {
      _repository.AddCoffeeOrder(coffee);
    }

    public IEnumerable<Coffee> Get()
    {
      return _repository.Get();
    }
  }
}