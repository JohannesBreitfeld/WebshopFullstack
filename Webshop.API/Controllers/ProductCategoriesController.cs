using Microsoft.AspNetCore.Mvc;
using Webshop.API.EntityMapping;
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
        var categories = await _service.GetAllAsync();
        var response = categories.MapToResponse();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound(new { message = "Product category not found" });
        }
        var response = category.MapToResponse();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCategoryRequest request)
    {
        var productCategory = request.MapToProductCategory();

        var success = await _service.CreateAsync(productCategory);
        if (!success)
        {
            return BadRequest(new { message = "Failed to create product category" });
        }

        var response = productCategory.MapToResponse();

        return CreatedAtAction(nameof(GetById), new { id = productCategory.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCategoryRequest request)
    {
        var productCategory = request.MapToProductCategory(id);
        var success = await _service.UpdateAsync(productCategory);
        if (!success)
        {
            return NotFound(new { message = "Category not found" });
        }
        var response = productCategory.MapToResponse();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) 
        { 
            return NotFound(new { message = "Category not found" }); 
        }

        return NoContent();
    }
}
