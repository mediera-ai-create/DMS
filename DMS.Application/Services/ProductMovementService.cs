using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DMS.Application.Services
{
    public class ProductMovementService : IProductMovementService
    {
        private readonly DmsDbContext _context;

        public ProductMovementService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<ProductMovement> AddMovementAsync(ProductMovementDto dto)
        {
            var movement = new ProductMovement
            {
                ProductId = dto.ProductId,
                MovementType = dto.MovementType,
                MovementDate = dto.MovementDate,
                Remarks = dto.Remarks
            };

            _context.ProductMovements.Add(movement);
            await _context.SaveChangesAsync();
            return movement;
        }

        public async Task<List<ProductMovement>> GetMovementsByProductAsync(int productId)
        {
            return await _context.ProductMovements
                .Where(m => m.ProductId == productId)
                .Include(m => m.Product)
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync();
        }
    }
}
