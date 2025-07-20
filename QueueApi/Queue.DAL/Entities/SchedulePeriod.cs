namespace Queue.DAL.Entities
{
    public class SchedulePeriod
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int GroupScheduleId { get; set; } 
        public GroupSchedule? GroupSchedule { get; set; }
    }
}
