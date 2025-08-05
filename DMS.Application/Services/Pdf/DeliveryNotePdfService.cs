using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using DMS.Models.Entities;

namespace DMS.Application.Services.Pdf
{
    public class DeliveryNotePdfService
    {
        public byte[] Generate(Sale sale)
        {
            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("DELIVERY NOTE").FontSize(20).Bold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Delivery Note #: DN-{sale.Id:D4}");
                        col.Item().Element(e =>
                            e.PaddingBottom(10).Text($"Delivery Date: {sale.DeliveredDate?.ToString("yyyy-MM-dd") ?? DateTime.UtcNow.ToString("yyyy-MM-dd")}")
                        );

                        col.Item().Text($"Customer: {sale.Customer.FullName}");
                        col.Item().Text($"Address: {sale.Customer.Address}");
                        col.Item().Text($"Product: {sale.Product.Name}");
                        col.Item().Text($"Dealer: {sale.Dealer.Name}");

                        col.Item().PaddingTop(20).Text("Product successfully delivered.").Bold();
                    });

                    page.Footer().AlignCenter().Text("Thank you for choosing us!");
                });
            }).GeneratePdf();
        }
    }
}
