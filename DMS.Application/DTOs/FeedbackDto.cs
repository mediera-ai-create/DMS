public class FeedbackDto
{
    public int DealerId { get; set; }
    public int CustomerId { get; set; }
    public int Rating { get; set; }
    public string? Comments { get; set; }
    public DateTime SubmittedAt { get; set; }
}
