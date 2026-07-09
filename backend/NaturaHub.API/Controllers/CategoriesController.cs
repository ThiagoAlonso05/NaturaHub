using Microsoft.AspNetCore.Mvc;
using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;
using NaturaHub.Application.Interfaces;

namespace NaturaHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    // A Injeção de Dependência na prática: a API pede um ICategoryService
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll([FromQuery] bool activeOnly = true)
    {
        var categories = await _categoryService.GetAllAsync(activeOnly);
        return Ok(categories); // Ok() retorna status HTTP 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        
        if (category == null) 
            return NotFound(); // Retorna status HTTP 404
            
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryRequest request)
    {
        var createdCategory = await _categoryService.CreateAsync(request);
        
        // CreatedAtAction retorna status 201 e um cabeçalho "Location" com a URL para acessar o item criado
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryResponse>> Update(int id, [FromBody] UpdateCategoryRequest request)
    {
        var updatedCategory = await _categoryService.UpdateAsync(id, request);
        
        if (updatedCategory == null)
            return NotFound();

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _categoryService.DeleteAsync(id);
        
        if (!success)
            return NotFound();

        return NoContent(); // Retorna 204: Deu certo, mas não tenho conteúdo pra devolver
    }
}
