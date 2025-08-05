using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Models.Entities;
using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DMS.Application.Services
{
    public class DealerService : IDealerService
    {
        private readonly DmsDbContext _context;

        public DealerService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dealer>> GetAllDealersAsync()
        {
            return await _context.Dealers.ToListAsync();
        }

        public async Task<Dealer?> GetByDealerIdAsync(int id)
        {
            return await _context.Dealers.FindAsync(id);            
        }

        public async Task<Dealer> AddDealerAsync(DealerDto dto)
        {
            var dealer = new Dealer
            {
                Name = dto.Name,
                Region = dto.Region,
                Contact = dto.Contact,
                CreatedAt = dto.CreatedAt
            };

            _context.Dealers.Add(dealer);
            await _context.SaveChangesAsync();

            return dealer;
        }

        public async Task<Dealer?> UpdateDealerAsync(int id, DealerDto dto)
        {
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null) return null;

            dealer.Name = dto.Name;
            dealer.Region = dto.Region;
            dealer.Contact = dto.Contact;

            await _context.SaveChangesAsync();
            return dealer;
        }

        public async Task<bool> DeleteDealerAsync(int id)
        {
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null) return false;

            _context.Dealers.Remove(dealer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
