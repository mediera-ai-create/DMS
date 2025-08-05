using DMS.Application.DTOs;
using DMS.Models.Entities;

namespace DMS.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetByProductIdAsync(int id);
        Task<Product> AddProductAsync(ProductDto product);
        Task<Product?> UpdateProductAsync(int id, ProductDto product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsNeedingReorderAsync();
        Task<IEnumerable<ProductDto>> FilterProductsAsync(string? status, int? dealerId, DateTime? fromDate, DateTime? toDate);
        Task<IEnumerable<object>> GetInventoryAgingReportAsync();
    }


}
