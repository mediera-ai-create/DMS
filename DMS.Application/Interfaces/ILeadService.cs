public interface ILeadService
{
    Task<IEnumerable<LeadDto>> GetAllAsync();
    Task<LeadDto?> GetByIdAsync(int id);
    Task<LeadDto> AddAsync(LeadDto dto);
    Task<bool> UpdateAsync(int id, LeadDto dto);
    Task<bool> DeleteAsync(int id);
}
