using Microsoft.AspNetCore.Mvc;
using Webshop.Application.Services;
using Webshop.Domain.Entities;

namespace Webshop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var success = await _service.CreateAsync(product);
        if (!success) return BadRequest("Failed to create product");

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }
}
