using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MaterialTypeController : ControllerBase
{
    private readonly IMaterialTypeService _svc;
    public MaterialTypeController(IMaterialTypeService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MaterialTypeDto dto) => Ok(await _svc.AddAsync(dto));

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());
}
