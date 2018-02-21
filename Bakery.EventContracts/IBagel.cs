using System;

namespace Bakery.EventContracts
{
  public interface IBagel
  {
    Guid Id { get; }
    string Type { get; }
    bool HasCreamCheese { get; }
    bool HasLox { get; }
  }
}