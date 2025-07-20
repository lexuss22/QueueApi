namespace Queue.DAL.Entities
{
    public class GroupSchedule
    {
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public List<SchedulePeriod?> Periods { get; set; }
    }
}
