using System;
using System.Linq;
using System.Threading.Tasks;
using Barista.Coordinator.Repository;
using Barista.EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Barista.Coordinator
{
  public class CoffeeCompletedConsumer : IConsumer<ICoffeeCompletedEvent>
  {
    private readonly IConfiguration _configuration;
    private readonly ICoffeeRepository _repository;

    public CoffeeCompletedConsumer(IConfiguration configuration, ICoffeeRepository repository)
    {
      _repository = repository;
      _configuration = configuration;
    }
    
    public async Task Consume(ConsumeContext<ICoffeeCompletedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Marking coffee {context.Message.Id} complete for {context.Message.OrderId}");

      var coffees = await _repository.Get();
      var coffee = coffees.FirstOrDefault(c => c.Id == context.Message.Id);
      coffee.IsComplete = true;
      await _repository.Update(coffee);
    }
  }
}