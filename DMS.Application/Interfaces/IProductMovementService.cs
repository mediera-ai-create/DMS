using DMS.Application.DTOs;
using DMS.Models.Entities;

namespace DMS.Application.Interfaces
{
    public interface IProductMovementService
    {
        Task<ProductMovement> AddMovementAsync(ProductMovementDto dto);
        Task<List<ProductMovement>> GetMovementsByProductAsync(int productId);
    }
}
