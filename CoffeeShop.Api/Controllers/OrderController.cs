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
  }
}