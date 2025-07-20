namespace Queue.DTO.Models
{
    public class ScheduleDTO
    {
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
    }
}
