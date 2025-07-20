using Queue.DAL.Entities;
using Queue.DTO.Models;

namespace Queue.DAL.Repositories.Interfaces
{
    public interface IQueueRepository
    {
        Task<IEnumerable<Group>> GetGropsByQuery(SearchGroupsQuery query);
        Task<bool> IsUnelectrorizedByGroup(string groupName);
        Task<Schedule?> UpdateSchedule(int id, ScheduleUpdateRequest request);
        Task<Group?> UpdateGroup(int id, GroupUpdateRequest request);
    }
}
