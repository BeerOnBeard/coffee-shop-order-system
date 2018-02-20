using System;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Coordinator.Repository;
using CoffeeShop.EventContracts;
using MassTransit;

namespace CoffeeShop.Coordinator
{
  public class OrderRequestedConsumer : IConsumer<IOrderRequestedEvent>
  {
    private readonly IOrderRepository _repository;
    public OrderRequestedConsumer(IOrderRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IOrderRequestedEvent> context)
    {
      await Console.Out.WriteLineAsync($"Received order {context.Message.Id}.");

      var order = new Order {
        Id = context.Message.Id,
        CustomerName = context.Message.CustomerName,
        Coffees = context.Message.Coffees.Select(coffee => new Order.Coffee {
          Id = coffee.Id,
          Type = coffee.Type,
          NumberOfSugars = coffee.NumberOfSugars,
          NumberOfCreamers = coffee.NumberOfCreamers
        }).ToList()
      };

      await _repository.Create(order);
    }
  }
}