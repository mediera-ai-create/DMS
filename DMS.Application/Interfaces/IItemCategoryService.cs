public interface IItemCategoryService
{
    Task<ItemCategoryDto> AddAsync(ItemCategoryDto dto);
    Task<IEnumerable<ItemCategoryDto>> GetAllAsync();
}
