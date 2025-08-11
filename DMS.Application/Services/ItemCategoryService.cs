using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class ItemCategoryService : IItemCategoryService
{
    private readonly DmsDbContext _context;
    public ItemCategoryService(DmsDbContext context) => _context = context;

    public async Task<ItemCategoryDto> AddAsync(ItemCategoryDto dto)
    {
        var e = new ItemCategory { Name = dto.Name };
        _context.Add(e);
        await _context.SaveChangesAsync();
        dto.Id = e.Id;
        return dto;
    }

    public async Task<IEnumerable<ItemCategoryDto>> GetAllAsync()
    {
        return await _context.Set<ItemCategory>()
            .Select(x => new ItemCategoryDto { Id = x.Id, Name = x.Name })
            .ToListAsync();
    }
}
