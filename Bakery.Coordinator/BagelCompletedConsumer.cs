using System;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Coordinator.Repository;
using Bakery.EventContracts;
using MassTransit;

namespace Bakery.Coordinator
{
  public class BagelCompletedConsumer : IConsumer<IBagelCompletedEvent>
  {
    private readonly IBagelRepository _repository;
    public BagelCompletedConsumer(IBagelRepository repository)
    {
      _repository = repository;
    }
    public async Task Consume(ConsumeContext<IBagelCompletedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Marking bagel {context.Message.Id} for order {context.Message.OrderId} as complete.");

      var bagels = await _repository.Get();
      var bagel = bagels.FirstOrDefault(b => b.Id == context.Message.Id);
      bagel.IsComplete = true;
      await _repository.Update(bagel);
    }
  }
}