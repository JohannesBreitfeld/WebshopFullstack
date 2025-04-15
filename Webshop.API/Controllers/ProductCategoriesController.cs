using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.ServiceInterfaces;

namespace Webshop.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryService _service;

    public ProductCategoriesController(IProductCategoryService productCategoryService)
    {
        _service = productCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _service.GetByIdAsync(id);
        
        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Product category with id {id} not found" });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody]CreateProductCategoryRequest request)
    {
        var response = await _service.CreateAsync(request);
        
        return response is not null
            ? CreatedAtAction(nameof(GetById), new { id = response.Id }, response)
            : BadRequest(new { message = "Failed to create product category" });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductCategoryRequest request)
    {
        var response = await _service.UpdateAsync(id, request);
        
        return response is not null
            ? Ok(response)
            : NotFound(new { message = $"Category with id {id} not found" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
      
        return success is true
            ? NoContent()
            : NotFound(new { message = $"Category with id {id} not found" });
    }
}
