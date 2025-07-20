namespace Queue.DTO.Models
{
    public class ScheduleUpdateRequest
    {
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
    }
}
