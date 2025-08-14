using DMS.Models.Entities;

public interface IItemService
{
    Task<ItemDto> AddItemAsync(ItemCreateDto dto);
    Task<ItemDto?> GetByIdAsync(int id);
    Task<IEnumerable<Item>> GetAllItemsAsync();
    // Task<IEnumerable<ItemDto>> GetByDealerAsync(int dealerId);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, ItemCreateDto dto);
}
