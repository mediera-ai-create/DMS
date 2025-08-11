public interface IMaterialTypeService
{
    Task<MaterialTypeDto> AddAsync(MaterialTypeDto dto);
    Task<IEnumerable<MaterialTypeDto>> GetAllAsync();
}
