using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.ServiceInterfaces;

namespace Webshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService customerService)
    {
        _service = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _service.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        if (customer == null) 
        { 
            return NotFound(new { message = $"Customer with id {id} not found" });
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = await _service.CreateAsync(request);
        if(customer is null)
        {
            return BadRequest(new { message = "Failed to create customer" });
        }
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequest request)
    {
        var updatedResponse = await _service.UpdateAsync(id, request);
        if (updatedResponse is null)
        {
            return NotFound(new { message = $"Customer with id {id} not found" });
        }
        return Ok(updatedResponse);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) 
        { 
            return NotFound(new { message = $"Customer with id {id} not found" }); 
        }
        return NoContent();
    }
}

