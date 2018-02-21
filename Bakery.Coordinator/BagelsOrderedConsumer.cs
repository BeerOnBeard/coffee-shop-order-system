using System;
using System.Threading.Tasks;
using Bakery.Coordinator.Repository;
using Bakery.EventContracts;
using MassTransit;

namespace Bakery.Coordinator
{
  public class BagelsOrderedConsumer : IConsumer<IBagelsOrderedEvent>
  {
    private readonly IBagelRepository _repository;

    public BagelsOrderedConsumer(IBagelRepository repository)
    {
      _repository = repository;
    }
    public async Task Consume(ConsumeContext<IBagelsOrderedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Bagels order received for order {context.Message.OrderId}.");

      foreach (var bagel in context.Message.Bagels)
      {
        await _repository.Add(new Bagel {
          OrderId = context.Message.OrderId,
          Id = bagel.Id,
          Type = bagel.Type,
          HasCreamCheese = bagel.HasCreamCheese,
          HasLox = bagel.HasLox
        });
      }
    }
  }
}