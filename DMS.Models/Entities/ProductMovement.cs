using DMS.Models.Entities;

public class ProductMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public required string MovementType { get; set; } // Inbound, Outbound
    public DateTime MovementDate { get; set; }
    public string? Remarks { get; set; }

    public Product Product { get; set; } = null!;
}
