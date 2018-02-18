using System;
using Automatonymous;
using MassTransit.Saga;

namespace CoffeeShop.Saga
{
  public class Order : SagaStateMachineInstance
  {
    public Guid CorrelationId { get; set; }
    public Guid? ExpirationId { get; set; }
    public State CurrentState { get; set; }
    public Guid? OrderId { get; set; }
    public string CustomerName { get; set; }
  }
}
