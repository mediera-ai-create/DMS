using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly IWebHostEnvironment _env;

    public ItemController(IItemService svc, IWebHostEnvironment env)
    {
        _itemService = svc;
        _env = env;
    }

    [HttpPost]
    [RequestSizeLimit(50_000_000)] // ~50MB adjust as necessary
    public async Task<IActionResult> Create(ItemCreateDto dto)
    {
        // Save files temporarily in a folder (we don't have itemId yet) - better pattern: save after item created
        // We'll first create an empty item, then save files to folder and attach them.

        //var attachments = new List<ItemAttachment>();

        //// create a temporary Item entity to save and get Id (use service, provide empty attachments)
        var created = await _itemService.AddItemAsync(dto);

        //var itemFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "items", created.Id.ToString());
        //if (!Directory.Exists(itemFolder)) Directory.CreateDirectory(itemFolder);

        //if (files != null && files.Any())
        //{
        //    foreach (var file in files)
        //    {
        //        var fileName = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
        //        var filePath = Path.Combine(itemFolder, fileName);
        //        using var fs = new FileStream(filePath, FileMode.Create);
        //        await file.CopyToAsync(fs);

        //        var relPath = $"/uploads/items/{created.Id}/{fileName}";
        //        attachments.Add(new ItemAttachment
        //        {
        //            FileName = file.FileName,
        //            FilePath = relPath,
        //            FileType = file.ContentType,
        //            UploadedAt = DateTime.UtcNow
        //        });
        //    }

        //    // update item with attachments
            await _itemService.UpdateAsync(created.Id, dto);
        //}

        var returned = await _itemService.GetByIdAsync(created.Id);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, returned);
    }

    //[HttpGet("dealer/{dealerId}")]
    // public async Task<IActionResult> GetByDealer(int dealerId) => Ok(await _itemService.GetByDealerAsync(dealerId));

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
            Ok(await _itemService.GetAllItemsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _itemService.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _itemService.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
