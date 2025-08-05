using DMS.Models.Entities;

public class Sale
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int DealerId { get; set; }
    public Dealer Dealer { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public required string Status { get; set; } // Booked, Approved, Invoiced, Delivered
    public decimal QuotationAmount { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? InvoicedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

}
