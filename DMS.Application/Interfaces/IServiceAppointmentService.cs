public interface IServiceAppointmentService
{
    Task<IEnumerable<ServiceAppointmentDto>> GetAllAsync();
    Task<ServiceAppointmentDto?> GetByIdAsync(int id);
    Task<ServiceAppointmentDto> AddAsync(ServiceAppointmentDto dto);
    Task<bool> UpdateAsync(int id, ServiceAppointmentDto dto);
    Task<bool> DeleteAsync(int id);
}
