public interface ICampaignService
{
    Task<IEnumerable<CampaignDto>> GetAllAsync();
    Task<CampaignDto?> GetByIdAsync(int id);
    Task<CampaignDto> AddAsync(CampaignDto dto);
    Task<bool> UpdateAsync(int id, CampaignDto dto);
    Task<bool> DeleteAsync(int id);
}
