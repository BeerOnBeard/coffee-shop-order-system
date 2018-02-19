using System;
using System.Linq;
using Automatonymous;
using CoffeeShop.EventContracts;

namespace CoffeeShop.Saga
{
  public class OrderStateMachine : MassTransitStateMachine<Order>
  {
    public State Active { get; private set; }
    public Event<IOrderCreatedEvent> OrderCreated { get; private set; }
    public Schedule<Order, IOrderExpiredEvent> OrderExpired { get; private set; }

    public OrderStateMachine()
    {
      InstanceState(order => order.CurrentState);

      Event(() => OrderCreated,
         config => config.CorrelateBy(order => order.OrderId, context => context.Message.Id).SelectId(context => context.Message.Id)
      );

      Schedule(() => OrderExpired, order => order.ExpirationId, schedule => {
        schedule.Delay = TimeSpan.FromMinutes(1);
        schedule.Received = e => e.CorrelateById(context => context.Message.OrderId);
      });

      Initially(
        When(OrderCreated)
          .Then(context => {
            context.Instance.OrderId = context.Data.Id;
            context.Instance.CustomerName = context.Data.CustomerName;
            context.Instance.Coffees = context.Data.Coffees.Select(coffee => new Order.Coffee {
              Id = coffee.Id,
              Type = coffee.Type,
              NumberOfSugars = coffee.NumberOfSugars,
              NumberOfCreamers = coffee.NumberOfCreamers
            });
          })
          .ThenAsync(context => Console.Out.WriteLineAsync($"Order {context.Instance.OrderId} started for {context.Instance.CustomerName}."))
          .Publish(context => new CoffeesOrderedEvent {
            OrderId = context.Instance.OrderId.Value,
            Coffees = context.Instance.Coffees.Select(coffee => new CoffeesOrderedEvent.CoffeeOrder {
              Id = coffee.Id,
              Type = coffee.Type,
              NumberOfSugars = coffee.NumberOfSugars,
              NumberOfCreamers = coffee.NumberOfCreamers
            })
           })
          .Schedule(OrderExpired, context => new OrderExpiredEvent(context.Instance))
          .TransitionTo(Active)
      );

      During(Active,
        When(OrderExpired.Received)
          .ThenAsync(context => Console.Out.WriteLineAsync($"Order {context.Instance.OrderId} timed out for {context.Instance.CustomerName}."))
          .Finalize()
      );

      SetCompletedWhenFinalized();
    }
  }
}