using FluentValidation;
using Queue.BLL.Services.Interfaces;
using Queue.BLL.Validators;
using Queue.DAL.Entities;
using Queue.DAL.Repositories.Interfaces;
using Queue.DTO.Models;

namespace Queue.BLL.Services
{
    public class QueueServices : IQueueServices
    {
        private readonly IQueueRepository _queueRepository;

        public QueueServices(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public async Task<IEnumerable<GroupDTO>> GetGroupsByParams(SearchGroupsRequest request)
        {
            if (!TimeSpan.TryParse(request.StartTime, out var start))
                throw new InvalidCastException("Invalid start time format");

            if (!TimeSpan.TryParse(request.FinishTime, out var finish))
                throw new InvalidCastException("Invalid finish time format");

            var groups = await _queueRepository.GetGropsByQuery(new SearchGroupsQuery
            {
                City = request.City,
                StartTime = start,
                FinishTime = finish
            });

            return MapToGroupDTOs(groups);
        }

        public Task<bool> IsUnelectrorizedByGroup(string groupName)
        {
            if(string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentException("Group name cannot be null or empty", nameof(groupName));

            return _queueRepository.IsUnelectrorizedByGroup(groupName);
        }

        public async Task<GroupDTO> UpdateGroup(int id, GroupUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Group update request cannot be null");

            var updatedGroup = await _queueRepository.UpdateGroup(id, request);

            if (updatedGroup == null)
                throw new KeyNotFoundException($"Group with ID {id} not found");

            return new GroupDTO
            {
                Name = updatedGroup.Name,
                Description = updatedGroup.Description
            };
        }

        public async Task<ScheduleDTO> UpdateSchedule(int id, ScheduleUpdateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Schedule update request cannot be null");

            var updatedSchedule = await _queueRepository.UpdateSchedule(id, request);

            if (updatedSchedule == null)
                throw new KeyNotFoundException($"Group with ID {id} not found");

            return new ScheduleDTO
            {
                Day = updatedSchedule.Day,
                StartTime = updatedSchedule.StartTime,
                FinishTime = updatedSchedule.FinishTime
            };
        }

        private IEnumerable<GroupDTO> MapToGroupDTOs(IEnumerable<Group> groups)
        {
            return groups.Select(g => new GroupDTO
            {
                Name = g.Name,
                Addresses = g.Addresses.Select(a => new AddressDTO
                {
                    AddressName = a.AddressName
                }).ToList(),
                Schedules = g.Schedules.Select(s => new ScheduleDTO
                {
                    Day = s.Day,
                    StartTime = s.StartTime,
                    FinishTime = s.FinishTime
                }).ToList()
            });
        }
    }
}
