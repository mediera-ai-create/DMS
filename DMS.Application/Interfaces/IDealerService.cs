using DMS.Application.DTOs;
using DMS.Models.Entities;

namespace DMS.Application.Interfaces
{
    public interface IDealerService
    {
        Task<IEnumerable<Dealer>> GetAllDealersAsync();
        Task<Dealer?> GetByDealerIdAsync(int id);
        Task<Dealer> AddDealerAsync(DealerDto dealer);
        Task<Dealer?> UpdateDealerAsync(int id, DealerDto dealer);
        Task<bool> DeleteDealerAsync(int id);
    }
}
