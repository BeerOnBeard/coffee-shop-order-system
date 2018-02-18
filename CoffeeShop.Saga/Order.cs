using System;
using System.Collections.Generic;
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
    public IEnumerable<Coffee> Coffees { get; set; }

    public class Coffee
    {
      public Guid Id { get; set; }
      public string Type { get; set; }
      public int NumberOfSugars { get; set; }
      public int NumberOfCreamers { get; set; }
    }
  }
}
