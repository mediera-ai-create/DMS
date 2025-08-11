using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _svc;
    private readonly IWebHostEnvironment _env;

    public ItemController(IItemService svc, IWebHostEnvironment env)
    {
        _svc = svc;
        _env = env;
    }

    [HttpPost]
    [RequestSizeLimit(50_000_000)] // ~50MB adjust as necessary
    public async Task<IActionResult> Create([FromForm] ItemCreateDto dto, List<IFormFile>? files)
    {
        // Save files temporarily in a folder (we don't have itemId yet) - better pattern: save after item created
        // We'll first create an empty item, then save files to folder and attach them.

        var attachments = new List<ItemAttachment>();

        // create a temporary Item entity to save and get Id (use service, provide empty attachments)
        var created = await _svc.AddItemAsync(dto, Enumerable.Empty<ItemAttachment>());

        var itemFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "items", created.Id.ToString());
        if (!Directory.Exists(itemFolder)) Directory.CreateDirectory(itemFolder);

        if (files != null && files.Any())
        {
            foreach (var file in files)
            {
                var fileName = $"{Guid.NewGuid():N}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(itemFolder, fileName);
                using var fs = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fs);

                var relPath = $"/uploads/items/{created.Id}/{fileName}";
                attachments.Add(new ItemAttachment
                {
                    FileName = file.FileName,
                    FilePath = relPath,
                    FileType = file.ContentType,
                    UploadedAt = DateTime.UtcNow
                });
            }

            // update item with attachments
            await _svc.UpdateAsync(created.Id, dto, attachments);
        }

        var returned = await _svc.GetByIdAsync(created.Id);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, returned);
    }

    [HttpGet("dealer/{dealerId}")]
   // public async Task<IActionResult> GetByDealer(int dealerId) => Ok(await _svc.GetByDealerAsync(dealerId));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _svc.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
