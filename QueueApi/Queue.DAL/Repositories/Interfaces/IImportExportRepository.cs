using Queue.DAL.Entities;
using Queue.DTO.Models;

namespace Queue.DAL.Repositories.Interfaces
{
    public interface IImportExportRepository
    {
        Task AddSchedulesAsync(List<GroupSchedule> schedules);
        Task<List<GroupSchedule>> GetAllSchedulesAsync();
    }
}
