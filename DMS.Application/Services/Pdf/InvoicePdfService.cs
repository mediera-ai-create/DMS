using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using DMS.Models.Entities;

namespace DMS.Application.Services.Pdf
{
    public class InvoicePdfService
    {
        public byte[] Generate(Sale sale)
        {
            var gstRate = 0.18m;
            var gstAmount = sale.QuotationAmount * gstRate;
            var totalAmount = sale.QuotationAmount + gstAmount;

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("INVOICE").FontSize(22).Bold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Invoice No: INV-{sale.Id:D4}").FontSize(12);
                        
                        col.Item().Text($"Invoice Date: {DateTime.UtcNow:yyyy-MM-dd}").FontSize(12);

                        col.Item().PaddingBottom(10).Text($"Address: {sale.Customer.Address}");

                        col.Item().PaddingBottom(10).Text($"Dealer: {sale.Dealer.Name} ({sale.Dealer.Region})");

                        col.Item().Text($"Customer: {sale.Customer.FullName}");
                        col.Item().Text($"Email: {sale.Customer.Email}");
                        col.Item().Text($"Product: {sale.Product.Name}");
                        col.Item().Text($"Base Price: ₹{sale.QuotationAmount:N0}");
                        col.Item().Text($"GST (18%): ₹{gstAmount:N0}");

                        col.Item().PaddingBottom(10).Text($"Total: ₹{totalAmount:N0}").Bold().FontSize(14);
                    });

                    page.Footer().AlignCenter().Text("Thank you for your business!").Italic();
                });
            });

            return document.GeneratePdf();
        }
    }
}
