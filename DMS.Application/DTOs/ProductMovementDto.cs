namespace DMS.Application.DTOs
{
    public class ProductMovementDto
    {
        public int ProductId { get; set; }
        public string MovementType { get; set; } = ""; // Inbound or Outbound
        public DateTime MovementDate { get; set; }
        public string? Remarks { get; set; }
    }
}
