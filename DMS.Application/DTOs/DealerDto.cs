namespace DMS.Application.DTOs
{
    public class DealerDto
    {
        public int Id { get; set; } // For update/delete
        public required string Name { get; set; }
        public required string Region { get; set; }
        public required string Contact { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Location { get; set; } = string.Empty; // Optional, can be empty
    }
}
