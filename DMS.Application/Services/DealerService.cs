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

        public async Task<IEnumerable<DealerDto>> GetAllAsync()
        {
            return await _context.Dealers
                .Select(d => new DealerDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Region = d.Region,
                    Contact = d.Contact,
                    CreatedAt = d.CreatedAt,
                    Location = d.Location ,
                    CreatedBy=d.CreatedBy// Assuming Location is a property in Dealer
                })
                .ToListAsync();
        }

        public async Task<DealerDto> GetByIdAsync(int id)
        {
            var d = await _context.Dealers.FindAsync(id);
            if (d == null) return null;

            return new DealerDto
            {
                Id = d.Id,
                Name = d.Name,
                Region = d.Region,
                Contact = d.Contact,
                CreatedAt = d.CreatedAt,
                Location = d.Location,
                CreatedBy=d.CreatedBy 
                // Assuming Location is a property in Dealer
            };
        }

        public async Task<DealerDto> AddAsync(DealerDto dto)
        {
            var dealer = new Dealer
            {
                Name = dto.Name,
                Region = dto.Region,
                Contact = dto.Contact,
                CreatedAt = dto.CreatedAt,
                Location = dto.Location // Assuming Location is a property in Dealer
            };

            _context.Dealers.Add(dealer);
            await _context.SaveChangesAsync();

            dto.Id = dealer.Id;
            return dto;
        }

        public async Task<DealerDto> UpdateAsync(int id, DealerDto dto)
        {
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null) return null;

            dealer.Name = dto.Name;
            dealer.Region = dto.Region;
            dealer.Contact = dto.Contact;
            dealer.CreatedBy = dto.CreatedBy;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null) return false;

            _context.Dealers.Remove(dealer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
