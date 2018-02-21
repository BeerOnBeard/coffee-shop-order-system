using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Api.Models;
using Bakery.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Api.Controllers
{
  [Route("/Bagels")]
  public class BagelsController : Controller
  {
    private readonly IBagelService _service;

    public BagelsController(IBagelService service)
    {
      _service = service;
    }

    [HttpGet]
    public IEnumerable<Bagel> Get()
    {
      return _service.Get();
    }
  }
}
