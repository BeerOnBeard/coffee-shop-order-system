using System;

namespace Barista.Coordinator.Repository
{
  public class Coffee
  {
    public Guid Id { get; set; }
    public Guid OriginalOrderId { get; set; }
    public string Type { get; set; }
    public int NumberOfSugars { get; set; }
    public int NumberOfCreamers { get; set; }
    public bool IsComplete { get; set; }
  }
}