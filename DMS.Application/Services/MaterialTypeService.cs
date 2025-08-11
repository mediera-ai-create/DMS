using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class MaterialTypeService : IMaterialTypeService
{
    private readonly DmsDbContext _context;
    public MaterialTypeService(DmsDbContext context) => _context = context;

    public async Task<MaterialTypeDto> AddAsync(MaterialTypeDto dto)
    {
        var e = new MaterialType { Name = dto.Name };
        _context.Add(e);
        await _context.SaveChangesAsync();
        dto.Id = e.Id;
        return dto;
    }

    public async Task<IEnumerable<MaterialTypeDto>> GetAllAsync()
    {
        return await _context.MaterialTypes
            .Select(m => new MaterialTypeDto { Id = m.Id, Name = m.Name })
            .ToListAsync();
    }
}
