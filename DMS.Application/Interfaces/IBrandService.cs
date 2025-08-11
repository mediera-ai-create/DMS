public interface IBrandService
{
    Task<BrandDto> AddAsync(BrandDto dto);
    Task<IEnumerable<BrandDto>> GetAllAsync();
    Task<BrandDto?> GetByIdAsync(int id);
}
