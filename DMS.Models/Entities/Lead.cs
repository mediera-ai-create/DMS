using DMS.Models.Entities;

public class Lead
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Source { get; set; } = string.Empty; // e.g., Website, Referral
    public string Status { get; set; } = "New"; // New, Contacted, Qualified, Lost, Converted
    public DateTime FollowUpDate { get; set; }
    public string? Notes { get; set; }

    public Customer? Customer { get; set; }
}
