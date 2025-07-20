using Queue.DTO.Models;

namespace Queue.BLL.Services.Interfaces
{
    public interface IQueueServices
    {
        Task<IEnumerable<GroupDTO>> GetGroupsByParams(SearchGroupsRequest request);
        Task<bool> IsUnelectrorizedByGroup(string groupName);
        Task<ScheduleDTO> UpdateSchedule(int id, ScheduleUpdateRequest request);
        Task<GroupDTO> UpdateGroup(int id, GroupUpdateRequest request);
    }
}
