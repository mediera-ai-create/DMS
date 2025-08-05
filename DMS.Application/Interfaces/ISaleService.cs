using DMS.Application.DTOs;
using DMS.Models.Entities;

namespace DMS.Application.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> GetAllSalesAsync();
        Task<Sale?> GetSaleByIdAsync(int id);
        Task<Sale> AddSaleAsync(SaleDto dto);
        Task<Sale?> UpdateSaleStatusAsync(int id, string status);
        Task<bool> DeleteSaleAsync(int id);
    }
}
