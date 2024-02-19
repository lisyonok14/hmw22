namespace hmw;

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.IO; 
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using System.Collections.Generic;


[ApiController]
public class StoreController : ControllerBase
{

    private readonly IOrderRepository _orderRepository;

    public StoreController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }




        [HttpPost]
        [Route("/store/add")]
        public IActionResult Add([FromBody] Order newOrder)
        { 
            _orderRepository.AddOrder(newOrder);
            return Ok(_orderRepository.GetAllOrders());
        }

        [HttpPost]
        [Route("/store/remove")]
        public IActionResult Delete(string name)
        {
            var order = _orderRepository.GetOrderByName(name);
            if (order != null)
            {
                _orderRepository.DeleteOrder(name);
                return Ok($"{name} удален навсегда");
            }
            else
            {
                return NotFound($"{name} не найден, но можете добавить");
            }
        }

        [HttpGet]
        [Route("/store/show")]
        public IActionResult Show()
        {
            return Ok(_orderRepository.GetAllOrders());
        }

}

