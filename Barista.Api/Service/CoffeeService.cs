using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task Complete(Coffee coffee)
    {
      coffee.IsComplete = true;
      await _publisher.PublishCompleted(coffee);
    }

    public async Task<IEnumerable<Coffee>> Get()
    {
      return await _repository.GetIncomplete();
    }
  }
}