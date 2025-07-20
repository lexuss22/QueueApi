namespace Queue.DTO.Models
{
    public class GroupScheduleDTO
    {
        public int GroupNumber { get; set; }
        public List<SchedulePeriodDTO> Periods { get; set; }
    }
}
