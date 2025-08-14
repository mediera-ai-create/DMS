using DMS.Application.DTOs;
using DMS.Models.Entities;

namespace DMS.Application.Interfaces
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<Activity?> GetActivityByIdAsync(int id);
        Task<Activity?> GetActivityByUserIdAsync(string userId);
        Task<Activity> AddActivityAsync(ActivityDto dto);
        Task<Activity?> UpdateActivityAsync(int id, ActivityDto dto);
        Task<bool> DeleteActivityAsync(int id);
    }
}
