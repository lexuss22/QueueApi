namespace Queue.DTO.Models
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AddressDTO> Addresses { get; set; }
        public List<ScheduleDTO> Schedules { get; set; }
    }
}
