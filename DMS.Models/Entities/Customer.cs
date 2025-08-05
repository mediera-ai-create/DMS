namespace DMS.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string ContactNumber { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    }
}
