using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barista.Api.Models;
using Barista.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Barista.Api.Controllers
{
  [Route("/Coffees")]
  public class CoffeesController : Controller
  {
    private readonly ICoffeeService _service;
    public CoffeesController(ICoffeeService service)
    {
      _service = service;
    }

    [HttpGet]
    public IEnumerable<Coffee> Get()
    {
      return _service.Get();
    }
  }
}
