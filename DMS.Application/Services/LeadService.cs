using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class LeadService : ILeadService
{
    private readonly DmsDbContext _context;

    public LeadService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeadDto>> GetAllAsync()
    {
        return await _context.Leads
            .Select(l => new LeadDto
            {
                Id = l.Id,
                CustomerId = l.CustomerId,
                Source = l.Source,
                Status = l.Status,
                FollowUpDate = l.FollowUpDate,
                Notes = l.Notes
            }).ToListAsync();
    }

    public async Task<LeadDto?> GetByIdAsync(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead == null) return null;

        return new LeadDto
        {
            Id = lead.Id,
            CustomerId = lead.CustomerId,
            Source = lead.Source,
            Status = lead.Status,
            FollowUpDate = lead.FollowUpDate,
            Notes = lead.Notes
        };
    }

    public async Task<LeadDto> AddAsync(LeadDto dto)
    {
        var lead = new Lead
        {
            CustomerId = dto.CustomerId,
            Source = dto.Source,
            Status = dto.Status,
            FollowUpDate = dto.FollowUpDate,
            Notes = dto.Notes
        };

        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();

        dto.Id = lead.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, LeadDto dto)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead == null) return false;

        lead.Source = dto.Source;
        lead.Status = dto.Status;
        lead.FollowUpDate = dto.FollowUpDate;
        lead.Notes = dto.Notes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead == null) return false;

        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
        return true;
    }
}
