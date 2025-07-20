namespace Queue.DAL.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressName { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
