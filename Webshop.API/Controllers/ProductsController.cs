using Microsoft.AspNetCore.Mvc;
using Webshop.API.EntityMapping;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.ServiceInterfaces;

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
        var products = await _service.GetAllAsync();
        var response = products.MapToResponse();
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _service.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        var response = product.MapToResponse();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var product = request.MapToProduct();

        var success = await _service.CreateAsync(product);
        if (!success)
        {
            return BadRequest("Failed to create product");
        }

        var response = product.MapToResponse();

        return CreatedAtAction(nameof(Get), new { id = product.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody]UpdateProductRequest request)
    {
        var product = request.MapToProduct(id);

        var result = await _service.UpdateAsync(product);
        if (!result)
        {
            return NotFound(); 
        }
        var response = product.MapToResponse();

        return Ok(response);
    }
}
