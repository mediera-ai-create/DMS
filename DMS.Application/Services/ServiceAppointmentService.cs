using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ServiceAppointmentService : IServiceAppointmentService
{
    private readonly DmsDbContext _context;

    public ServiceAppointmentService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceAppointmentDto>> GetAllAsync()
    {
        return await _context.ServiceAppointments
            .Select(a => new ServiceAppointmentDto
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                ProductId = a.ProductId,
                ScheduledDate = a.ScheduledDate,
                Status = a.Status,
                Remarks = a.Remarks,
                CreatedAt = a.CreatedAt
            }).ToListAsync();
    }

    public async Task<ServiceAppointmentDto?> GetByIdAsync(int id)
    {
        var a = await _context.ServiceAppointments.FindAsync(id);
        if (a == null) return null;

        return new ServiceAppointmentDto
        {
            Id = a.Id,
            CustomerId = a.CustomerId,
            ProductId = a.ProductId,
            ScheduledDate = a.ScheduledDate,
            Status = a.Status,
            Remarks = a.Remarks,
            CreatedAt = a.CreatedAt
        };
    }

    public async Task<ServiceAppointmentDto> AddAsync(ServiceAppointmentDto dto)
    {
        var entity = new ServiceAppointment
        {
            CustomerId = dto.CustomerId,
            ProductId = dto.ProductId,
            ScheduledDate = dto.ScheduledDate,
            Status = dto.Status,
            Remarks = dto.Remarks,
            CreatedAt = DateTime.UtcNow
        };

        _context.ServiceAppointments.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        dto.CreatedAt = entity.CreatedAt;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, ServiceAppointmentDto dto)
    {
        var entity = await _context.ServiceAppointments.FindAsync(id);
        if (entity == null) return false;

        entity.ScheduledDate = dto.ScheduledDate;
        entity.Status = dto.Status;
        entity.Remarks = dto.Remarks;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ServiceAppointments.FindAsync(id);
        if (entity == null) return false;

        _context.ServiceAppointments.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
