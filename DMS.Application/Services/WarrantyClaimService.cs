using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class WarrantyClaimService : IWarrantyClaimService
{
    private readonly DmsDbContext _context;

    public WarrantyClaimService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WarrantyClaimDto>> GetAllAsync()
    {
        return await _context.WarrantyClaims
            .Select(c => new WarrantyClaimDto
            {
                Id = c.Id,
                JobCardId = c.JobCardId,
                IssueDescription = c.IssueDescription,
                Status = c.Status,
                ClaimDate = c.ClaimDate,
                ResolutionNotes = c.ResolutionNotes
            }).ToListAsync();
    }

    public async Task<WarrantyClaimDto?> GetByIdAsync(int id)
    {
        var claim = await _context.WarrantyClaims.FindAsync(id);
        if (claim == null) return null;

        return new WarrantyClaimDto
        {
            Id = claim.Id,
            JobCardId = claim.JobCardId,
            IssueDescription = claim.IssueDescription,
            Status = claim.Status,
            ClaimDate = claim.ClaimDate,
            ResolutionNotes = claim.ResolutionNotes
        };
    }

    public async Task<WarrantyClaimDto> AddAsync(WarrantyClaimDto dto)
    {
        var claim = new WarrantyClaim
        {
            JobCardId = dto.JobCardId,
            IssueDescription = dto.IssueDescription,
            Status = dto.Status,
            ClaimDate = dto.ClaimDate,
            ResolutionNotes = dto.ResolutionNotes
        };

        _context.WarrantyClaims.Add(claim);
        await _context.SaveChangesAsync();

        dto.Id = claim.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, WarrantyClaimDto dto)
    {
        var claim = await _context.WarrantyClaims.FindAsync(id);
        if (claim == null) return false;

        claim.IssueDescription = dto.IssueDescription;
        claim.Status = dto.Status;
        claim.ResolutionNotes = dto.ResolutionNotes;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var claim = await _context.WarrantyClaims.FindAsync(id);
        if (claim == null) return false;

        _context.WarrantyClaims.Remove(claim);
        await _context.SaveChangesAsync();
        return true;
    }
}
