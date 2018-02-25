using System;
using System.Linq;
using CoffeeShop.Api.Models;
using CoffeeShop.Api.Repository;
using CoffeeShop.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
  [Route("/Orders/{id}")]
  public class OrderController
  {
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
      _service = service;
    }

    [HttpGet]
    public Order Get(Guid id)
    {
      return _service.Get().FirstOrDefault(order => order.Id == id);
    }

    /// <summary>
    /// Kind of a hack, but gets the point across. This could use media-types to
    /// signify intent (example: application/vnd.cs.order-fulfilled+json) or the
    /// system could infer events depending on change. For example, read current
    /// state from data store, if isFulfilled changes from false to true then 
    /// raise the event for order fulfilled notification. For now, hack hack hack...
    /// </summary>
    [HttpPost]
    public void Fulfilled(Guid id)
    {
      _service.MarkFulfilled(id);
    }
  }
}