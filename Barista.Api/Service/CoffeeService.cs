using System;
using System.Collections.Generic;
using System.Linq;
using Barista.Api.Eventing;
using Barista.Api.Models;
using Barista.Api.Repository;

namespace Barista.Api.Service
{
  public class CoffeeService : ICoffeeService
  {
    private readonly ICoffeeRepository _repository;
    private readonly ICoffeeEventPublisher _publisher;

    public CoffeeService(ICoffeeRepository repository, ICoffeeEventPublisher publisher)
    {
      _publisher = publisher;
      _repository = repository;
    }

    public void AddCoffeeOrder(Coffee coffee)
    {
      _repository.AddCoffeeOrder(coffee);
    }

    public void Complete(Guid coffeeId)
    {
      var coffee = _repository.Get().FirstOrDefault(c => c.Id == coffeeId);
      if (coffee == null)
      {
        return;
      }

      coffee.IsComplete = true;

      _publisher.PublishCompleted(coffee);
      _repository.Update(coffee);
    }

    public IEnumerable<Coffee> Get()
    {
      return _repository.Get();
    }
  }
}