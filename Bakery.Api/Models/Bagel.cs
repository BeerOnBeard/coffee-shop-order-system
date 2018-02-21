using System;

namespace Bakery.Api.Models
{
  public class Bagel
  {
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Type { get; set; }
    public bool HasCreamCheese { get; set; }
    public bool HasLox { get; set; }
    public bool IsComplete { get; set; }
  }
}