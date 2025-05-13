using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        var response = await _service.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var response = await _service.GetByIdAsync(id);

        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Product with id {id} not found" }); 
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName([FromRoute]string name)
    {
        var response = await _service.GetByNameAsync(name);
        
        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Product with name {name} not found" });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
    {
        var response = await _service.CreateAsync(request);
    
        return response is not null
            ? CreatedAtAction(nameof(GetById), new { id = response.Id }, response)
            : BadRequest(new { message = "Failed to create product" });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct([FromRoute]int id, [FromBody]UpdateProductRequest request)
    {
        var response = await _service.UpdateAsync(id, request);
       
        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Product with id {id} not found" });
    }

    [HttpDelete("soft-delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SoftDelete([FromRoute] int id)
    {
        var success = await _service.SoftDeleteAsync(id);

        return success is true
            ? NoContent()
            : NotFound(new { message = $"Product with id {id} not found" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct([FromRoute]int id)
    {
        var success = await _service.DeleteAsync(id);

        return success is true
            ? NoContent()
            : NotFound(new { message = $"Product with id {id} not found" });
    }

    [HttpPost("get-by-ids")]
    public async Task<IActionResult> GetByIds([FromBody]GetProductsByIdsRequest request)
    {
        var response = await _service.GetByIdsAsync(request);
        return Ok(response);
    }
}
