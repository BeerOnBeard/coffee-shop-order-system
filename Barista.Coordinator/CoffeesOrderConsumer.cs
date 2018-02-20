using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Barista.Coordinator.Repository;
using Barista.EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Barista.Coordinator
{
  public class CoffeesOrderConsumer : IConsumer<ICoffeesOrderedEvent>
  {
    private readonly IConfiguration _configuration;
    private readonly ICoffeeRepository _repository;

    public CoffeesOrderConsumer(IConfiguration configuration, ICoffeeRepository repository)
    {
      _repository = repository;
      _configuration = configuration;
    }
    
    public async Task Consume(ConsumeContext<ICoffeesOrderedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Received order for {context.Message.OrderId}");

      foreach (var coffee in context.Message.Coffees)
      {
        await _repository.AddCoffeeOrder(new Coffee {
          Id = coffee.Id,
          OriginalOrderId = context.Message.OrderId,
          Type = coffee.Type,
          NumberOfSugars = coffee.NumberOfSugars,
          NumberOfCreamers = coffee.NumberOfCreamers
        });
      }
    }
  }
}