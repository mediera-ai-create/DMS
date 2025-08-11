using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DimensionController : ControllerBase
{
    private readonly IDimensionService _svc;
    public DimensionController(IDimensionService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DimensionDto dto) => Ok(await _svc.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var d = await _svc.GetByIdAsync(id);
        return d == null ? NotFound() : Ok(d);
    }
}
