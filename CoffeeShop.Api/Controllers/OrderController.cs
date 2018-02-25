using System;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Api.Models;
using CoffeeShop.Api.Repository;
using CoffeeShop.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
  [Route("/Orders/{id}")]
  public class OrderController : Controller
  {
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
      _service = service;
    }

    /// <summary>
    /// Kind of a hack, but gets the point across. This could use media-types to
    /// signify intent (example: application/vnd.cs.order-fulfilled+json) or the
    /// system could infer events depending on change. For example, read current
    /// state from data store, if isFulfilled changes from false to true then 
    /// raise the event for order fulfilled notification. For now, hack hack hack...
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Fulfilled(Guid id)
    {
      await _service.MarkFulfilled(id);
      return Ok();
    }
  }
}