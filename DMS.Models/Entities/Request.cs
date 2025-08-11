
public class Request
{
    public int Id { get; set; }

    // Generic dimension fields (store as numbers where applicable)
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
