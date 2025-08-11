using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly DmsDbContext _context;

        public RequestService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequestDto>> GetAllAsync()
        {
            return await _context.Requests
                .Select(r => new RequestDto
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();
        }

        public async Task<RequestDto> GetByIdAsync(int id)
        {
            var req = await _context.Requests.FindAsync(id);
            if (req == null) return null;

            return new RequestDto
            {
                Id = req.Id,
                Name = req.Name
            };
        }

        public async Task<RequestDto> AddAsync(RequestCreateDto dto)
        {
            var req = new Request
            {
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(req);
            await _context.SaveChangesAsync();

            return new RequestDto
            {
                Id = req.Id,
                Name = req.Name
            };
        }

        public async Task<RequestDto> UpdateAsync(int id, RequestCreateDto dto)
        {
            var req = await _context.Requests.FindAsync(id);
            if (req == null) return null;

            req.Name = dto.Name;
            await _context.SaveChangesAsync();

            return new RequestDto
            {
                Id = req.Id,
                Name = req.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var req = await _context.Requests.FindAsync(id);
            if (req == null) return false;

            _context.Requests.Remove(req);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
