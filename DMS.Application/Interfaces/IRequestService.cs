using DMS.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.Application.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDto>> GetAllAsync();
        Task<RequestDto> GetByIdAsync(int id);
        Task<RequestDto> AddAsync(RequestCreateDto dto);
        Task<RequestDto> UpdateAsync(int id, RequestCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
