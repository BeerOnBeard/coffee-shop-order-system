using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.Api.Models;
using CoffeeShop.Api.Repository;
using CoffeeShop.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
  [Route("/Orders")]
  public class OrdersController : Controller
  {
    private readonly IOrderService _service;
    
    public OrdersController(IOrderService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> Get()
    {
      return await _service.Get();
    }

    [HttpPost]
    public async Task<Order> AddNewOrder([FromBody]CreateOrder order)
    {
      return await _service.Create(order);
    }
  }
}
