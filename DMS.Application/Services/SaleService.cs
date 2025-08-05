using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DMS.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly DmsDbContext _context;

        public SaleService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task<Sale> AddSaleAsync(SaleDto dto)
        {
            // Optional: Validate foreign keys exist before creating the sale
            var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);
            var dealerExists = await _context.Dealers.AnyAsync(d => d.Id == dto.DealerId);
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId);

            if (!productExists || !dealerExists || !customerExists)
            {
                throw new ArgumentException("Invalid ProductId, DealerId, or CustomerId.");
            }

            var sale = new Sale
            {
                ProductId = dto.ProductId,
                DealerId = dto.DealerId,
                CustomerId = dto.CustomerId,
                Status = dto.Status,
                QuotationAmount = dto.QuotationAmount,
                BookingDate = dto.BookingDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return sale;
        }


        public async Task<Sale?> UpdateSaleStatusAsync(int id, string status)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;

            sale.Status = status;
            if (status == "Delivered") sale.DeliveryDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
