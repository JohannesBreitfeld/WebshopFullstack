using Microsoft.AspNetCore.Mvc;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.ServiceInterfaces;

namespace Webshop.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService orderService)
    {
        _service = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var response = await _service.AddAsync(request);

        return response is not null
            ? Ok("Order created")
            : BadRequest(new { message = "Failed to create order" });
    }
}
