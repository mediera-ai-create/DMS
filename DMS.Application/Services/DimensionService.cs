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
            Size = dto.Size,
            Thickness = dto.Thickness,
            Length = dto.Length,
            Width = dto.Width,
            Diameter = dto.Diameter
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
                Size = d.Size,
                Thickness = d.Thickness,
                Length = d.Length,
                Width = d.Width,
                Diameter = d.Diameter,
                CreatedAt = d.CreatedAt
            }).ToListAsync();
    }

    public async Task<DimensionDto?> GetByIdAsync(int id)
    {
        var d = await _context.Dimensions.FindAsync(id);
        if (d == null) return null;
        return new DimensionDto
        {
            Id = d.Id,
            Size = d.Size,
            Thickness = d.Thickness,
            Length = d.Length,
            Width = d.Width,
            Diameter = d.Diameter,
            CreatedAt = d.CreatedAt
        };
    }
}
