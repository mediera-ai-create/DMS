using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Models.Entities;
using DMS.Infrastructure.Data;

namespace DMS.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly DmsDbContext _context;

        public ActivityService(DmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _context.Activities
                .ToListAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(int id)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Activity?> GetActivityByUserIdAsync(string userId)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<Activity> AddActivityAsync(ActivityDto dto)
        {
            var dealer = await _context.Dealers.FindAsync(dto.DealerId);
            var request = await _context.Requests.FindAsync(dto.RequestId);
            var item = await _context.Items.FindAsync(dto.ItemId);

            if (dealer == null || request == null || item == null)
                throw new InvalidOperationException("Dealer, Request, or Item not found.");

            var activity = new Activity
            {
                DealerId = dto.DealerId,
                DealerName = dealer.Name,
                UserId = dto.UserId,
                RequestId = dto.RequestId,
                RequestName = request.Name,
                ItemId = dto.ItemId,
                ItemName = item.Name,
                AdditionalFiles = dto.AdditionalFiles,
                Remarks = dto.Remarks,
                PhotoPath = dto.PhotoPath,
                NextFollowUpDate = dto.NextFollowUpDate
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<Activity?> UpdateActivityAsync(int id, ActivityDto dto)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return null;
            var dealer = await _context.Dealers.FindAsync(dto.DealerId);
            var request = await _context.Requests.FindAsync(dto.RequestId);
            var item = await _context.Items.FindAsync(dto.ItemId);

            if (dealer == null || request == null || item == null)
                throw new InvalidOperationException("Dealer, Request, or Item not found.");

            activity.DealerId = dto.DealerId;
            activity.DealerName = dealer.Name;
            activity.UserId = dto.UserId;
            activity.RequestId = dto.RequestId;
            activity.RequestName = request.Name;
            activity.ItemId = dto.ItemId;
            activity.ItemName = item.Name;
            activity.AdditionalFiles = dto.AdditionalFiles;
            activity.Remarks = dto.Remarks;
            activity.PhotoPath = dto.PhotoPath;
            activity.NextFollowUpDate = dto.NextFollowUpDate;

            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteActivityAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return false;

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
