using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CampaignService : ICampaignService
{
    private readonly DmsDbContext _context;

    public CampaignService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CampaignDto>> GetAllAsync()
    {
        return await _context.Campaigns
            .Select(c => new CampaignDto
            {
                Id = c.Id,
                Name = c.Name,
                Channel = c.Channel,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                Notes = c.Notes
            }).ToListAsync();
    }

    public async Task<CampaignDto?> GetByIdAsync(int id)
    {
        var c = await _context.Campaigns.FindAsync(id);
        if (c == null) return null;

        return new CampaignDto
        {
            Id = c.Id,
            Name = c.Name,
            Channel = c.Channel,
            StartDate = c.StartDate,
            EndDate = c.EndDate,
            Status = c.Status,
            Notes = c.Notes
        };
    }

    public async Task<CampaignDto> AddAsync(CampaignDto dto)
    {
        var campaign = new Campaign
        {
            Name = dto.Name,
            Channel = dto.Channel,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            Notes = dto.Notes
        };

        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();

        dto.Id = campaign.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, CampaignDto dto)
    {
        var c = await _context.Campaigns.FindAsync(id);
        if (c == null) return false;

        c.Name = dto.Name;
        c.Channel = dto.Channel;
        c.StartDate = dto.StartDate;
        c.EndDate = dto.EndDate;
        c.Status = dto.Status;
        c.Notes = dto.Notes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var c = await _context.Campaigns.FindAsync(id);
        if (c == null) return false;

        _context.Campaigns.Remove(c);
        await _context.SaveChangesAsync();
        return true;
    }
}
