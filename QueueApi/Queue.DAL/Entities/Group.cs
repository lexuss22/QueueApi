namespace Queue.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
