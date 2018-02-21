using System;
using Bakery.Api.Models;
using Bakery.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Api.Controllers
{
  [Route("/Bagels/{id}")]
  public class BagelController : Controller
  {
    private readonly IBagelService _service;
    public BagelController(IBagelService service)
    {
      _service = service;
    }

    /// <summary>
    /// This should really be an update, but for simplicity (and POC!)
    /// I'm just going to complete the order on POST.
    /// </summary>
    [HttpPost]
    public void Complete(Guid id, [FromBody]Bagel bagel)
    {
      _service.Complete(bagel);
    }
  }
}