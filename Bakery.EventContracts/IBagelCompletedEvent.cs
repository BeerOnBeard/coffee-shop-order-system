using System;

namespace Bakery.EventContracts
{
  public interface IBagelCompletedEvent
  {
    Guid Id { get; set; }
    Guid OrderId { get; set; }
  }
}