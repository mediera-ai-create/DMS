using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class DimensionService : IDimensionService
{
    private readonly DmsDbContext _context;
    public DimensionService(DmsDbContext context) => _context = context;

    public async Task<DimensionDto> AddAsync(DimensionDto dto)
    {
        var e = new Dimension
        {
            Name = dto.Name
            
        };
        _context.Add(e);
        await _context.SaveChangesAsync();
        dto.Id = e.Id;
        dto.CreatedAt = e.CreatedAt;
        return dto;
    }

    public async Task<IEnumerable<DimensionDto>> GetAllAsync()
    {
        return await _context.Dimensions
            .Select(d => new DimensionDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
    }

    public async Task<DimensionDto?> GetByIdAsync(int id)
    {
        var d = await _context.Dimensions.FindAsync(id);
        if (d == null) return null;
        return new DimensionDto
        {
            Id = d.Id,
            Name = d.Name,
            CreatedAt = d.CreatedAt
        };
    }
}
