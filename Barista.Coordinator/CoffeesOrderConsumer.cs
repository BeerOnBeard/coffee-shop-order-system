using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Barista.EventContracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Barista.Coordinator
{
  public class CoffeesOrderConsumer : IConsumer<ICoffeesOrderedEvent>
  {
    private readonly IConfiguration _configuration;

    public CoffeesOrderConsumer(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public Task Consume(ConsumeContext<ICoffeesOrderedEvent> context)
    {
      return Task.Run(async () =>
      {
        await Console.Out.WriteLineAsync($"Received order for {context.Message.OrderId}");
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(_configuration["Barista:Url"]);
          
          foreach (var coffee in context.Message.Coffees)
          {
            var body = new {
              coffee.Id,
              coffee.Type,
              coffee.NumberOfSugars,
              coffee.NumberOfCreamers
            };

            await client.PostAsync(
              "Coffees",
              new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
          }
        }
      });
    }
  }
}