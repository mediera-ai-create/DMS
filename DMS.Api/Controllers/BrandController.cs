using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _svc;
    public BrandController(IBrandService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BrandDto dto) => Ok(await _svc.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var b = await _svc.GetByIdAsync(id);
        return b == null ? NotFound() : Ok(b);
    }
}
