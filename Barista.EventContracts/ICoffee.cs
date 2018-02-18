using System;

namespace Barista.EventContracts
{
  public interface ICoffee
  {
    Guid Id { get; }
    string Type { get; }
    int NumberOfSugars { get; }
    int NumberOfCreamers { get; }
  }
}