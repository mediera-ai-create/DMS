using DMS.Application.DTOs;

namespace DMS.Application.Interfaces
{
    public interface IDealerService
    {
        Task<IEnumerable<DealerDto>> GetAllAsync();
        Task<DealerDto> GetByIdAsync(int id);
        Task<DealerDto> AddAsync(DealerDto dealer);
        Task<DealerDto> UpdateAsync(int id, DealerDto dealer);
        Task<bool> DeleteAsync(int id);
    }
}
