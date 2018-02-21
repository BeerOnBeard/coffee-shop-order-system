using System;

namespace CoffeeShop.EventContracts
{
  public interface IBagel
  {
    Guid Id { get; }
    string Type { get; }
    bool HasCreamCheese { get; }
    bool HasLox { get; }
  }
}