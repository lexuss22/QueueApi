namespace Queue.DTO.Models
{
    public class UpdateResponse<T>
    {
        public  string Message { get; set; }
        public T Data { get; set; }
    }
}
