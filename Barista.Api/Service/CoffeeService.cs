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

    public void Complete(Coffee coffee)
    {
      coffee.IsComplete = true;
      _publisher.PublishCompleted(coffee);
    }

    public IEnumerable<Coffee> Get()
    {
      return _repository.Get();
    }
  }
}