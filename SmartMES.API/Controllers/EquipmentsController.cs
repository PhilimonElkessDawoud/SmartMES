using Microsoft.AspNetCore.Mvc;
using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;

namespace SmartMES.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentsController : ControllerBase
{
    private readonly IEquipmentService _service;

    public EquipmentsController(IEquipmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("by-production-line/{productionLineId}")]
    public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetByProductionLine(int productionLineId)
    {
        return Ok(await _service.GetByProductionLineIdAsync(productionLineId));
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentDto>> Create(CreateEquipmentDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEquipmentDto dto)
    {
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}