using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.Api.Models;
using CoffeeShop.Api.Repository;
using CoffeeShop.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
  [Route("/Orders")]
  public class OrdersController
  {
    private readonly IOrderService _service;
    
    public OrdersController(IOrderService service)
    {
      _service = service;
    }

    [HttpGet]
    public IEnumerable<Order> Get()
    {
      return _service.Get().ToList();
    }

    [HttpPost]
    public Order AddNewOrder([FromBody]CreateOrder order)
    {
      return _service.Create(order);
    }
  }
}
