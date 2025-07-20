using Microsoft.EntityFrameworkCore;
using Queue.DAL.Entities;


namespace Queue.DAL.Context
{
    public class ImportExportDbContext : DbContext
    {
        public ImportExportDbContext(DbContextOptions<ImportExportDbContext> options) : base(options)
        {
        }
        public DbSet<GroupSchedule> GroupSchedules { get; set; }
        public DbSet<SchedulePeriod> SchedulePeriods { get; set; }
    }
}
