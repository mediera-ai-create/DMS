public class WarrantyClaim
{
    public int Id { get; set; }
    public int JobCardId { get; set; }
    public string IssueDescription { get; set; } = string.Empty;
    public string Status { get; set; } = "Submitted"; // Submitted, Approved, Rejected
    public DateTime ClaimDate { get; set; }
    public string? ResolutionNotes { get; set; }

    public JobCard? JobCard { get; set; }
}
