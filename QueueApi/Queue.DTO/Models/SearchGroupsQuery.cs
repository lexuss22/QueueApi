namespace Queue.DTO.Models
{
    public class SearchGroupsQuery
    {
        public string City { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
    }
}
