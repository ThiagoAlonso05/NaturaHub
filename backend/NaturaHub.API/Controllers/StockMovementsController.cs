using Microsoft.AspNetCore.Mvc;
using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;
using NaturaHub.Application.Interfaces;

namespace NaturaHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockMovementsController : ControllerBase
{
    private readonly IStockMovementService _movementService;

    public StockMovementsController(IStockMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<StockMovementResponse>>> GetHistory(int productId)
    {
        var history = await _movementService.GetHistoryByProductIdAsync(productId);
        return Ok(history);
    }

    [HttpPost]
    public async Task<ActionResult<StockMovementResponse>> Create([FromBody] CreateStockMovementRequest request)
    {
        try
        {
            var movement = await _movementService.CreateAsync(request);
            
            // Aqui não retornamos CreatedAtAction porque não criamos um endpoint GetById pra movimentação.
            // Para o MVP, Ok() resolve perfeitamente.
            return Ok(movement);
        }
        catch (InvalidOperationException ex) // Ex: "Estoque insuficiente"
        {
            // BadRequest (400) ou UnprocessableEntity (422) seriam ideais. Usaremos 400 por simplicidade.
            return BadRequest(new { Error = ex.Message });
        }
        catch (ArgumentException ex) // Ex: "Produto não existe"
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StockMovementResponse>> Update(int id, [FromBody] UpdateStockMovementRequest request)
    {
        try
        {
            var updatedMovement = await _movementService.UpdateAsync(id, request);
            
            if (updatedMovement == null)
                return NotFound();

            return Ok(updatedMovement);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
