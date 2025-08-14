using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class ItemService : IItemService
{
    private readonly DmsDbContext _context;
    public ItemService(DmsDbContext context) => _context = context;

    public async Task<ItemDto> AddItemAsync(ItemCreateDto dto)
    {
        var item = new Item
        {
            
            BrandId = dto.BrandId,
            CategoryId = dto.CategoryId,
            MaterialTypeId = dto.MaterialTypeId,
            Name = dto.Name,
            Grade = dto.Grade,
            HasTestCertificate = dto.HasTestCertificate,
            Dimension1Id = dto.Dimension1Id,
            Dimension2Id = dto.Dimension2Id,
            Dimension3Id = dto.Dimension3Id,
            Dimension1Value = dto.Dimension1Value,
            Dimension2Value = dto.Dimension2Value,
            Dimension3Value = dto.Dimension3Value,
            Attachments = dto.Attachments,
            CreatedAt = DateTime.UtcNow
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        //foreach (var a in attachments)
        //{
        //    a.ItemId = item.Id;
        //    _context.ItemAttachments.Add(a);
        //}

        //await _context.SaveChangesAsync();
        var result = await GetByIdAsync(item.Id);
        return result!;
    }

    public async Task<IEnumerable<Dealer>> GetAllItemsAsync()
    {
        return await _context.Dealers.ToListAsync();
    }
    public async Task<ItemDto?> GetByIdAsync(int id)
    {
        var i = await _context.Items
            .Include(x => x.Brand)
            .Include(x => x.Category)
            .Include(x => x.MaterialType)
            //.Include(x => x.Attachments)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (i == null) return null;

        return new ItemDto
        {
            Id = i.Id,
            BrandId = i.BrandId,
            BrandName = i.Brand?.CompanyName ?? string.Empty,
            CategoryId = i.CategoryId,
            CategoryName = i.Category?.Name ?? string.Empty,
            MaterialTypeId = i.MaterialTypeId,
            MaterialTypeName = i.MaterialType?.Name ?? string.Empty,
            Name = i.Name,
            Grade = i.Grade,
            HasTestCertificate = i.HasTestCertificate,
            CreatedAt = i.CreatedAt,
            Dimension1Value = i.Dimension1Value,
            Dimension2Value = i.Dimension2Value,
            Dimension3Value = i.Dimension3Value,
            Attachments = i.Attachments
        };
    }

    //public async Task<IEnumerable<ItemDto>> GetByDealerAsync(int dealerId)
    //{
    //    var items = await _context.Items
    //        .Where(x => x.DealerId == dealerId)
    //        .Include(x => x.Brand)
    //        .Include(x => x.Category)
    //        .Include(x => x.MaterialType)
    //        .Include(x => x.Attachments)
    //        .ToListAsync();

    //    return items.Select(i => new ItemDto
    //    {
    //        Id = i.Id,
    //        DealerId = i.DealerId,
    //        BrandId = i.BrandId,
    //        BrandName = i.Brand?.CompanyName ?? string.Empty,
    //        CategoryId = i.CategoryId,
    //        CategoryName = i.Category?.Name ?? string.Empty,
    //        MaterialTypeId = i.MaterialTypeId,
    //        MaterialTypeName = i.MaterialType?.Name ?? string.Empty,
    //        Name = i.Name,
    //        Grade = i.Grade,
    //        HasTestCertificate = i.HasTestCertificate,
    //        CreatedAt = i.CreatedAt,
    //        Attachments = i.Attachments.Select(a => new ItemAttachmentDto { Id = a.Id, FileName = a.FileName, FilePath = a.FilePath, FileType = a.FileType, UploadedAt = a.UploadedAt }).ToList()
    //    });
    //}

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.Items.Include(i => i.Attachments).FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return false;

        // delete files from disk if needed (controller could handle) - here we remove DB records
        //_context.ItemAttachments.RemoveRange(item.Attachments);
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(int id, ItemCreateDto dto)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return false;

        item.BrandId = dto.BrandId;
        item.CategoryId = dto.CategoryId;
        item.MaterialTypeId = dto.MaterialTypeId;
        item.Name = dto.Name;
        item.Grade = dto.Grade;
        item.HasTestCertificate = dto.HasTestCertificate;
        item.Dimension1Id = dto.Dimension1Id;
        item.Dimension2Id = dto.Dimension2Id;
        item.Dimension3Id = dto.Dimension3Id;
        item.Dimension1Value = dto.Dimension1Value;
        item.Dimension2Value = dto.Dimension2Value;
        item.Dimension3Value = dto.Dimension3Value;
        item.Attachments = dto.Attachments;


        // add new attachments
        //foreach (var a in attachments)
        //{
        //    a.ItemId = item.Id;
        //    _context.ItemAttachments.Add(a);
        //}

        await _context.SaveChangesAsync();
        return true;
    }
}
