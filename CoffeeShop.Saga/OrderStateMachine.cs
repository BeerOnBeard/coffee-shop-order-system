using System;
using System.Linq;
using Automatonymous;
using Bakery.EventContracts;
using Barista.EventContracts;
using CoffeeShop.EventContracts;

namespace CoffeeShop.Saga
{
  public class OrderStateMachine : MassTransitStateMachine<Order>
  {
    public State Active { get; private set; }
    public Event<IOrderRequestedEvent> OrderRequested { get; private set; }
    public Event<IOrderCompletedEvent> OrderCompleted { get; private set; }
    public Event<ICoffeeCompletedEvent> CoffeeCompleted { get; private set; }
    public Event<IBagelCompletedEvent> BagelCompleted { get; private set; }

    public Schedule<Order, IOrderExpiredEvent> OrderExpired { get; private set; }

    public OrderStateMachine()
    {
      InstanceState(order => order.CurrentState);

      Event(() => OrderRequested,
         config => config.CorrelateBy(order => order.OrderId, context => context.Message.Id).SelectId(context => context.Message.Id)
      );

      Event(() => CoffeeCompleted,
        config => config.CorrelateBy(order => order.OrderId, context => context.Message.OrderId)
      );

      Event(() => BagelCompleted,
        config => config.CorrelateBy(order => order.OrderId, context => context.Message.OrderId)
      );

      Event(() => OrderCompleted,
        config => config.CorrelateBy(order => order.OrderId, context => context.Message.Id)
      );

      Schedule(() => OrderExpired, order => order.ExpirationId, schedule => {
        schedule.Delay = TimeSpan.FromMinutes(10);
        schedule.Received = e => e.CorrelateById(context => context.Message.OrderId);
      });

      Initially(
        When(OrderRequested)
          .Then(context => {
            context.Instance.OrderId = context.Data.Id;
            context.Instance.CustomerName = context.Data.CustomerName;
            context.Instance.Coffees = context.Data.Coffees.Select(coffee => new Order.Coffee {
              Id = coffee.Id,
              Type = coffee.Type,
              NumberOfSugars = coffee.NumberOfSugars,
              NumberOfCreamers = coffee.NumberOfCreamers
            }).ToList();
            context.Instance.Bagels = context.Data.Bagels.Select(bagel => new Order.Bagel {
              Id = bagel.Id,
              Type = bagel.Type,
              HasCreamCheese = bagel.HasCreamCheese,
              HasLox = bagel.HasLox
            }).ToList();
          })
          .ThenAsync(context => Console.Out.WriteLineAsync($"Order {context.Instance.OrderId} started for {context.Instance.CustomerName}."))
          .Publish(context => new CoffeesOrderedEvent {
            OrderId = context.Instance.OrderId.Value,
            Coffees = context.Instance.Coffees.Select(coffee => new CoffeesOrderedEvent.Coffee {
              Id = coffee.Id,
              Type = coffee.Type,
              NumberOfSugars = coffee.NumberOfSugars,
              NumberOfCreamers = coffee.NumberOfCreamers
            })
           })
           .Publish(context => new BagelsOrderedEvent {
             OrderId = context.Instance.OrderId.Value,
             Bagels = context.Instance.Bagels.Select(bagel => new BagelsOrderedEvent.Bagel {
               Id = bagel.Id,
               Type = bagel.Type,
               HasCreamCheese = bagel.HasCreamCheese,
               HasLox = bagel.HasLox
             })
           })
          .Schedule(OrderExpired, context => new OrderExpiredEvent(context.Instance))
          .TransitionTo(Active)
      );

      During(Active,
        When(CoffeeCompleted)
          .ThenAsync(context => Console.Out.WriteLineAsync($"Coffee {context.Data.Id} completed for order {context.Instance.OrderId}"))
          .Then(context => context.Instance.Coffees.Single(c => c.Id == context.Data.Id).IsComplete = true)
          .Schedule(OrderExpired, context => new OrderExpiredEvent(context.Instance))
          .If(context => context.Instance.Coffees.All(c => c.IsComplete)
                      && context.Instance.Bagels.All(b => b.IsComplete),
              binder => binder.Publish(context => new OrderCompletedEvent { Id = context.Instance.CorrelationId })
          ),
        When(BagelCompleted)
          .ThenAsync(context => Console.Out.WriteLineAsync($"Bagel {context.Data.Id} completed for order {context.Instance.OrderId}"))
          .Then(context => context.Instance.Bagels.Single(b => b.Id == context.Data.Id).IsComplete = true)
          .Schedule(OrderExpired, context => new OrderExpiredEvent(context.Instance))
          .If(context => context.Instance.Coffees.All(c => c.IsComplete)
                      && context.Instance.Bagels.All(b => b.IsComplete),
              binder => binder.Publish(context => new OrderCompletedEvent { Id = context.Instance.CorrelationId })
          ),
        When(OrderCompleted)
          .ThenAsync(context => Console.Out.WriteLineAsync($"Order {context.Instance.OrderId} completed!"))
          .Unschedule(OrderExpired)
          .Finalize(),
        When(OrderExpired.Received)
          .ThenAsync(context => Console.Out.WriteLineAsync($"Order {context.Instance.OrderId} timed out for {context.Instance.CustomerName}."))
          .Finalize()
      );

      SetCompletedWhenFinalized();
    }
  }
}