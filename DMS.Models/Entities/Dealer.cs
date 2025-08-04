namespace DMS.Models.Entities
{
    public class Dealer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Region { get; set; }
        public required string Contact { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Location { get; set; }
        public string? CreatedBy { get; set; } 
        public string? UpdatedBy { get; set; } 
    }
}
