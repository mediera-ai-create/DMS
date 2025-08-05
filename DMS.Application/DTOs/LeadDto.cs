public class LeadDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    public DateTime FollowUpDate { get; set; }
    public string? Notes { get; set; }
}
