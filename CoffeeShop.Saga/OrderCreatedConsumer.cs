using System;
using System.Threading.Tasks;
using CoffeeShop.EventContracts;
using MassTransit;

namespace CoffeeShop.Saga
{
  public class OrderCreatedConsumer : IConsumer<IOrderCreatedEvent>
  {
    public Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
      return Task.Run(() => Console.WriteLine($"Order {context.Message.Id} Created!"));
    }
  }
}