using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Models.Entities;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMS.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly DmsDbContext _context;

        public ProductService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByProductIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);            
        }

        public async Task<Product> AddProductAsync(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,    
                Model = dto.Model,
                Make = dto.Make,
                VIN = dto.VIN,
                Status = dto.Status,
                DealerId = dto.DealerId,
                ArrivalDate = dto.ArrivalDate,
                CreatedAt = DateTime.Now,
                ReorderLevel = dto.ReorderLevel
            };

            // Save product first to get ID
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Now calculate stock and reorder flag
            product.IsReorderRequired = await CalculateReorderStatusAsync(product.Id, product.ReorderLevel);

            // Update with reorder flag
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Model = dto.Model;
            product.Make = dto.Make;
            product.VIN = dto.VIN;
            product.Status = dto.Status;
            product.DealerId = dto.DealerId;
            product.ArrivalDate = dto.ArrivalDate;
            //product.IsReorderRequired = dto.IsReorderRequired;
            product.IsReorderRequired = await CalculateReorderStatusAsync(id, dto.ReorderLevel);
            product.ReorderLevel = dto.ReorderLevel;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RecalculateReorderStatusForAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                var inbound = await _context.ProductMovements
                    .CountAsync(m => m.ProductId == product.Id && m.MovementType == "Inbound");
                var outbound = await _context.ProductMovements
                    .CountAsync(m => m.ProductId == product.Id && m.MovementType == "Outbound");
                var stock = inbound - outbound;

                product.IsReorderRequired = stock <= product.ReorderLevel;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsNeedingReorderAsync()
        {
            var products = await _context.Products
                .Where(p => p.IsReorderRequired)
                .ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                Make = p.Make,
                VIN = p.VIN,
                Status = p.Status,
                ArrivalDate = p.ArrivalDate,
                DealerId = p.DealerId,
                CreatedAt = p.CreatedAt,
                ReorderLevel = p.ReorderLevel,
                IsReorderRequired = p.IsReorderRequired
            });
        }

        public async Task<IEnumerable<ProductDto>> FilterProductsAsync(string? status, int? dealerId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(p => p.Status == status);

            if (dealerId.HasValue)
                query = query.Where(p => p.DealerId == dealerId.Value);

            if (fromDate.HasValue)
                query = query.Where(p => p.ArrivalDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(p => p.ArrivalDate <= toDate.Value);

            var list = await query.ToListAsync();

            return list.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                Make = p.Make,
                VIN = p.VIN,
                Status = p.Status,
                DealerId = p.DealerId,
                ArrivalDate = p.ArrivalDate,
                ReorderLevel = p.ReorderLevel,
                IsReorderRequired = p.IsReorderRequired,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<IEnumerable<object>> GetInventoryAgingReportAsync()
        {
            var today = DateTime.UtcNow;

            var result = await _context.Products
                .Where(p => p.Status == "InStock")
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Model,
                    p.Make,
                    p.Status,
                    DaysInStock = (int)(today.Date - ((DateTime)p.ArrivalDate).Date).TotalDays,
                    p.ArrivalDate
                }).ToListAsync();


            return result;
        }

        private async Task<bool> CalculateReorderStatusAsync(int productId, int reorderLevel)
        {
            var inbound = await _context.ProductMovements
                .CountAsync(m => m.ProductId == productId && m.MovementType == "Inbound");

            var outbound = await _context.ProductMovements
                .CountAsync(m => m.ProductId == productId && m.MovementType == "Outbound");

            var currentStock = inbound - outbound;
            return currentStock <= reorderLevel;
        }

    }
}
