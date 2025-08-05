using DMS.Application.DTOs;
using DMS.Application.Interfaces;
using DMS.Application.Services.Pdf;
using DMS.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly DmsDbContext _context;

        public SaleController(ISaleService saleService, DmsDbContext context)
        {
            _saleService = saleService;
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _saleService.GetAllSalesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _saleService.GetSaleByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleDto dto)
        {
            var result = await _saleService.AddSaleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var result = await _saleService.UpdateSaleStatusAsync(id, status);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteSaleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/quotation")]
        public async Task<IActionResult> GetQuotationPdf(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.Dealer)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null) return NotFound();

            var pdfService = new QuotationPdfService();
            var pdfBytes = pdfService.Generate(sale);

            return File(pdfBytes, "application/pdf", $"Quotation_SALE_{id}.pdf");
        }

        [HttpGet("{id}/invoice")]
        public async Task<IActionResult> GetInvoicePdf(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.Dealer)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null) return NotFound();

            var pdfService = new InvoicePdfService();
            var pdfBytes = pdfService.Generate(sale);

            return File(pdfBytes, "application/pdf", $"Invoice_INV_{id:D4}.pdf");
        }

        [HttpPut("{id}/status/approve")]
        public async Task<IActionResult> ApproveSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return NotFound();

            sale.Status = "Approved";
            sale.ApprovedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok($"Sale {id} approved.");
        }

        [HttpPut("{id}/status/invoice")]
        public async Task<IActionResult> InvoiceSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return NotFound();

            sale.Status = "Invoiced";
            sale.InvoicedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok($"Sale {id} invoiced.");
        }

        [HttpPut("{id}/status/deliver")]
        public async Task<IActionResult> DeliverSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return NotFound();

            sale.Status = "Delivered";
            sale.DeliveredDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok($"Sale {id} marked as delivered.");
        }

        [HttpGet("{id}/delivery-note")]
        public async Task<IActionResult> GetDeliveryNotePdf(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Product)
                .Include(s => s.Dealer)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null) return NotFound();

            var pdfService = new DeliveryNotePdfService();
            var pdfBytes = pdfService.Generate(sale);

            return File(pdfBytes, "application/pdf", $"DeliveryNote_DN_{id:D4}.pdf");
        }



    }
}
