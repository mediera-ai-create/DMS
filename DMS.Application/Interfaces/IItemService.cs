public interface IItemService
{
    Task<ItemDto> AddItemAsync(ItemCreateDto dto, IEnumerable<ItemAttachment> attachments);
    Task<ItemDto?> GetByIdAsync(int id);
   // Task<IEnumerable<ItemDto>> GetByDealerAsync(int dealerId);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, ItemCreateDto dto, IEnumerable<ItemAttachment> attachments);
}
