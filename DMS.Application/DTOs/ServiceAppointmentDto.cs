public class ServiceAppointmentDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public string Status { get; set; } = "Scheduled";
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
}
