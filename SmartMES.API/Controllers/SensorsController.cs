using Microsoft.AspNetCore.Mvc;
using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;

namespace SmartMES.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ISensorAppService _service;

    public SensorsController(ISensorAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SensorDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("by-equipment/{equipmentId}")]
    public async Task<ActionResult<IEnumerable<SensorDto>>> GetByEquipment(int equipmentId)
    {
        return Ok(await _service.GetByEquipmentIdAsync(equipmentId));
    }

    [HttpPost]
    public async Task<ActionResult<SensorDto>> Create(CreateSensorDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSensorDto dto)
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