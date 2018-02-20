using System;
using CoffeeShop.EventContracts;

namespace CoffeeShop.Saga
{
  public class OrderCompletedEvent : IOrderCompletedEvent
  {
    public Guid Id { get; set; }
  }
}