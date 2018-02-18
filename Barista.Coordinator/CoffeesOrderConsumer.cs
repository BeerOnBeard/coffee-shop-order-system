using System;
using System.Threading.Tasks;
using Barista.EventContracts;
using MassTransit;

namespace Barista.Coordinator
{
  public class CoffeesOrderConsumer : IConsumer<ICoffeesOrderedEvent>
  {
    public Task Consume(ConsumeContext<ICoffeesOrderedEvent> context)
    {
      return Console.Out.WriteLineAsync($"Received order for {context.Message.OrderId}");
    }
  }
}