using System;
using System.Threading.Tasks;
using CoffeeShop.Coordinator.Repository;
using CoffeeShop.EventContracts;
using MassTransit;

namespace CoffeeShop.Coordinator
{
  public class OrderCompletedConsumer : IConsumer<IOrderCompletedEvent>
  {
    private readonly IOrderRepository _repository;
    public OrderCompletedConsumer(IOrderRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IOrderCompletedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Received complete notification for order {context.Message.Id}.");

      var order = await _repository.Get(context.Message.Id);
      if (order == null)
      {
        await Console.Out.WriteLineAsync($"Could not find the order {context.Message.Id}!");
      }

      order.IsComplete = true;
      await _repository.Update(order);
    }
  }
}