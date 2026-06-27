using Microsoft.AspNetCore.Mvc;
using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;

namespace SmartMES.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly IAlertService _service;

    public AlertsController(IAlertService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlertDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("unresolved")]
    public async Task<ActionResult<IEnumerable<AlertDto>>> GetUnresolved()
    {
        return Ok(await _service.GetUnresolvedAsync());
    }

    [HttpPut("{id}/resolve")]
    public async Task<IActionResult> Resolve(int id)
    {
        var success = await _service.ResolveAsync(id);
        return success ? NoContent() : NotFound();
    }
}