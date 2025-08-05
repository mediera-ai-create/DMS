using DMS.Models.Entities;

public class Feedback
{
    public int Id { get; set; }
    public int DealerId { get; set; }
    public Dealer Dealer { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int Rating { get; set; } // 1 to 5
    public string? Comments { get; set; }
    public DateTime SubmittedAt { get; set; }
}
