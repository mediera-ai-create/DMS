public class JobCardDto
{
    public int Id { get; set; }
    public int ServiceAppointmentId { get; set; }
    public string MechanicName { get; set; } = string.Empty;
    public string WorkDescription { get; set; } = string.Empty;
    public decimal EstimatedCost { get; set; }
    public string? PartsUsed { get; set; }
    public DateTime CreatedAt { get; set; }
}
