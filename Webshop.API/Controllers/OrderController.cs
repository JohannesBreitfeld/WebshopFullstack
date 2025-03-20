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
            ? CreatedAtAction(nameof(GetByOrderId), new { orderId = response.Id }, response)
            : BadRequest(new { message = "Failed to create order" });
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetByOrderId([FromRoute] int orderId)
    {
        var response = await _service.GetByOrderIdAsync(orderId);

        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Could not find order with id {orderId}" });
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetByCustomerId([FromRoute] int customerId)
    {
        var response = await _service.GetByCustomerIdAsync(customerId);

        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Could not find any orders with for customer id {customerId}" });
    }
}
