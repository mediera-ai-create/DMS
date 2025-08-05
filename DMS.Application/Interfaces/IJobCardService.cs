public interface IJobCardService
{
    Task<IEnumerable<JobCardDto>> GetAllAsync();
    Task<JobCardDto?> GetByIdAsync(int id);
    Task<JobCardDto> AddAsync(JobCardDto dto);
    Task<bool> UpdateAsync(int id, JobCardDto dto);
    Task<bool> DeleteAsync(int id);
}
