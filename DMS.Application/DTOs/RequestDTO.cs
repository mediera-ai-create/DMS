namespace DMS.Application.DTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RequestCreateDto
    {
        public string Name { get; set; }
    }
}
