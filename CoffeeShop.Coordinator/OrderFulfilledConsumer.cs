using System;
using System.Threading.Tasks;
using CoffeeShop.Coordinator.Repository;
using CoffeeShop.EventContracts;
using MassTransit;

namespace CoffeeShop.Coordinator
{
  public class OrderFulfilledConsumer : IConsumer<IOrderFulfilledEvent>
  {
    private readonly IOrderRepository _repository;
    public OrderFulfilledConsumer(IOrderRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IOrderFulfilledEvent> context)
    {
      await Console.Out.WriteLineAsync($"Received fulfilled notification for order {context.Message.Id}.");

      var order = await _repository.Get(context.Message.Id);
      if (order == null)
      {
        await Console.Out.WriteLineAsync($"Order {context.Message.Id} could not be found when trying to consume IOrderFulfilledEvent.");
        return;
      }

      order.IsFulfilled = true;
      await _repository.Update(order);
    }
  }
}