public interface IDimensionService
{
    Task<DimensionDto> AddAsync(DimensionDto dto);
    Task<IEnumerable<DimensionDto>> GetAllAsync();
    Task<DimensionDto?> GetByIdAsync(int id);
}
