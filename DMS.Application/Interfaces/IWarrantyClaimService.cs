public interface IWarrantyClaimService
{
    Task<IEnumerable<WarrantyClaimDto>> GetAllAsync();
    Task<WarrantyClaimDto?> GetByIdAsync(int id);
    Task<WarrantyClaimDto> AddAsync(WarrantyClaimDto dto);
    Task<bool> UpdateAsync(int id, WarrantyClaimDto dto);
    Task<bool> DeleteAsync(int id);
}
