namespace DMS.Application.DTOs
{
    public class SaleDto
    {
        public int ProductId { get; set; }
        public int DealerId { get; set; }
        public int CustomerId { get; set; }
        public required string Status { get; set; }
        public decimal QuotationAmount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
