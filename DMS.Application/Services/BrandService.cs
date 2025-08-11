using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class BrandService : IBrandService
{
    private readonly DmsDbContext _context;
    public BrandService(DmsDbContext context) => _context = context;

    public async Task<BrandDto> AddAsync(BrandDto dto)
    {
        var b = new Brand
        {
            CompanyName = dto.CompanyName,
            Address1 = dto.Address1,
            Address2 = dto.Address2,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            GSTIN = dto.GSTIN
        };
        _context.Brands.Add(b);
        await _context.SaveChangesAsync();
        dto.Id = b.Id;
        return dto;
    }

    public async Task<IEnumerable<BrandDto>> GetAllAsync()
    {
        return await _context.Brands.Select(b => new BrandDto
        {
            Id = b.Id,
            CompanyName = b.CompanyName,
            Address1 = b.Address1,
            Address2 = b.Address2,
            City = b.City,
            State = b.State,
            Country = b.Country,
            GSTIN = b.GSTIN
        }).ToListAsync();
    }

    public async Task<BrandDto?> GetByIdAsync(int id)
    {
        var b = await _context.Brands.FindAsync(id);
        if (b == null) return null;
        return new BrandDto
        {
            Id = b.Id,
            CompanyName = b.CompanyName,
            Address1 = b.Address1,
            Address2 = b.Address2,
            City = b.City,
            State = b.State,
            Country = b.Country,
            GSTIN = b.GSTIN
        };
    }
}
