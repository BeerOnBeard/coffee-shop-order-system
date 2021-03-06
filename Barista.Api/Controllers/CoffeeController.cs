using System;
using System.Threading.Tasks;
using Barista.Api.Models;
using Barista.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Barista.Api.Controllers
{
  [Route("/Coffees/{id}")]
  public class CoffeeController : Controller
  {
    private readonly ICoffeeService _service;
    public CoffeeController(ICoffeeService service)
    {
      _service = service;
    }

    /// <summary>
    /// This should really be an update, but for simplicity (and POC!)
    /// I'm just going to complete the order on POST.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Complete(Guid id, [FromBody]Coffee coffee)
    {
      await _service.Complete(coffee);
      return NoContent();
    }
  }
}