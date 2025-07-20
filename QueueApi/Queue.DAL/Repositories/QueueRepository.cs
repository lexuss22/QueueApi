using Microsoft.EntityFrameworkCore;
using Queue.DAL.Context;
using Queue.DAL.Entities;
using Queue.DAL.Repositories.Interfaces;
using Queue.DTO.Models;

namespace Queue.DAL.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueDbContext _context;

        public QueueRepository(QueueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGropsByQuery(SearchGroupsQuery query)
        {
            
            var groups = await _context.Groups
                .Include(g => g.Addresses)
                .Include(g => g.Schedules)
                .Where(g => g.Addresses.Any(a => a.AddressName.Contains(query.City)) &&
                            g.Schedules.Any(s => s.StartTime <= query.FinishTime && s.FinishTime >= query.StartTime))
                .ToListAsync();

            return groups;
        }

        public async Task<bool> IsUnelectrorizedByGroup(string groupName)
        {
            var currentDayTime = DateTime.UtcNow;
            var currentDay = currentDayTime.DayOfWeek;
            var currentTime = currentDayTime.TimeOfDay;

            return await _context.Groups
                .Include(g => g.Schedules)
                .SelectMany(g => g.Schedules)
                .Where(s => s.Day.ToLower() == currentDay.ToString().ToLower())
                .AnyAsync(s =>
                    (s.StartTime <= s.FinishTime && currentTime >= s.StartTime && currentTime <= s.FinishTime) ||
                    (s.StartTime > s.FinishTime && (currentTime >= s.StartTime || currentTime <= s.FinishTime))
                );


        }

        public async Task<Group?> UpdateGroup(int id, GroupUpdateRequest request)
        {
            var groupDomain = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (groupDomain != null)
            {
                groupDomain.Name = request.Name;
                groupDomain.Description = request.Description;
                
                await _context.SaveChangesAsync();
                return groupDomain;
            }
            return null;
        }

        public async Task<Schedule> UpdateSchedule(int id, ScheduleUpdateRequest request)
        {
            var scheduleDomain = await _context.Schedules.FirstOrDefaultAsync(g => g.GroupId == id);
            if (scheduleDomain != null)
            
            scheduleDomain.Day = request.Day;
            scheduleDomain.StartTime = request.StartTime;
            scheduleDomain.FinishTime = request.FinishTime;

            await _context.SaveChangesAsync();
            return scheduleDomain;
            

            return null;
        }
    }
}
