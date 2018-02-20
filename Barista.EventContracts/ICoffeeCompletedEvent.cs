using System;

namespace Barista.EventContracts
{
  public interface ICoffeeCompletedEvent
  {
    Guid Id { get; }
    Guid OrderId { get; }
  }
}