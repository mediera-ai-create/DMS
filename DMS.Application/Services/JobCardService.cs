using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class JobCardService : IJobCardService
{
    private readonly DmsDbContext _context;

    public JobCardService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobCardDto>> GetAllAsync()
    {
        return await _context.JobCards
            .Select(j => new JobCardDto
            {
                Id = j.Id,
                ServiceAppointmentId = j.ServiceAppointmentId,
                MechanicName = j.MechanicName,
                WorkDescription = j.WorkDescription,
                EstimatedCost = j.EstimatedCost,
                PartsUsed = j.PartsUsed,
                CreatedAt = j.CreatedAt
            }).ToListAsync();
    }

    public async Task<JobCardDto?> GetByIdAsync(int id)
    {
        var job = await _context.JobCards.FindAsync(id);
        if (job == null) return null;

        return new JobCardDto
        {
            Id = job.Id,
            ServiceAppointmentId = job.ServiceAppointmentId,
            MechanicName = job.MechanicName,
            WorkDescription = job.WorkDescription,
            EstimatedCost = job.EstimatedCost,
            PartsUsed = job.PartsUsed,
            CreatedAt = job.CreatedAt
        };
    }

    public async Task<JobCardDto> AddAsync(JobCardDto dto)
    {
        var entity = new JobCard
        {
            ServiceAppointmentId = dto.ServiceAppointmentId,
            MechanicName = dto.MechanicName,
            WorkDescription = dto.WorkDescription,
            EstimatedCost = dto.EstimatedCost,
            PartsUsed = dto.PartsUsed,
            CreatedAt = DateTime.UtcNow
        };

        _context.JobCards.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        dto.CreatedAt = entity.CreatedAt;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, JobCardDto dto)
    {
        var job = await _context.JobCards.FindAsync(id);
        if (job == null) return false;

        job.MechanicName = dto.MechanicName;
        job.WorkDescription = dto.WorkDescription;
        job.EstimatedCost = dto.EstimatedCost;
        job.PartsUsed = dto.PartsUsed;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var job = await _context.JobCards.FindAsync(id);
        if (job == null) return false;

        _context.JobCards.Remove(job);
        await _context.SaveChangesAsync();
        return true;
    }
}
