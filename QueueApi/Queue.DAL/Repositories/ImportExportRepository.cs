using Microsoft.EntityFrameworkCore;
using Queue.DAL.Context;
using Queue.DAL.Entities;
using Queue.DAL.Repositories.Interfaces;
using Queue.DTO.Models;
using System.Text;

namespace Queue.DAL.Repositories
{
    public class ImportExportRepository : IImportExportRepository
    {
        private readonly ImportExportDbContext _context;

        public ImportExportRepository(ImportExportDbContext context)
        {
            _context = context;
        }
        public async Task AddSchedulesAsync(List<GroupSchedule> schedules)
        {
            _context.SchedulePeriods.RemoveRange(_context.SchedulePeriods);
            _context.RemoveRange(_context.GroupSchedules);
            await _context.GroupSchedules.AddRangeAsync(schedules);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GroupSchedule>> GetAllSchedulesAsync()
        {
            return await _context.GroupSchedules
            .Include(g => g.Periods)
            .ToListAsync();
        }

        private string GetFullExceptionMessage(Exception ex)
        {
            var sb = new StringBuilder();
            while (ex != null)
            {
                sb.AppendLine(ex.Message);
                ex = ex.InnerException;
            }
            return sb.ToString();
        }
    }
}
