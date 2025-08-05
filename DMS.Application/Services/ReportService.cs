using DMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore; // Ensure this is present

public class ReportService : IReportService
{
    private readonly DmsDbContext _context;

    public ReportService(DmsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalesReportDto>> GetSalesPerformanceByDealerAsync()
    {
        return await _context.Sales
            .Include(s => s.Dealer)
            .GroupBy(s => new { s.DealerId, s.Dealer.Name, s.Dealer.Region })
            .Select(g => new SalesReportDto
            {
                DealerName = g.Key.Name,
                Region = g.Key.Region,
                TotalSales = g.Count(),
                TotalRevenue = g.Sum(s => s.QuotationAmount)
            }).ToListAsync();
    }

    public async Task<IEnumerable<InventoryAgingDto>> GetInventoryAgingAsync()
    {
        var today = DateTime.UtcNow;

        var inventory = await _context.Products
            .Where(p => p.Status == "InStock")
            .Join(_context.Dealers,
                  p => p.DealerId,
                  d => d.Id,
                  (p, d) => new { Product = p, Dealer = d })
            .ToListAsync();

        return inventory.Select(pd => new InventoryAgingDto
        {
            ProductName = pd.Product.Name,
            VIN = pd.Product.VIN,
            DealerName = pd.Dealer.Name,
            Status = pd.Product.Status,
            DaysInInventory = (today - pd.Product.ArrivalDate).Days
        });
    }


    public async Task<IEnumerable<CustomerSatisfactionDto>> GetCustomerSatisfactionTrendsAsync()
    {
        return await _context.Feedbacks
            .Include(f => f.Dealer)
            .GroupBy(f => new { f.Dealer.Id, f.Dealer.Name })
            .Select(g => new CustomerSatisfactionDto
            {
                DealerName = g.Key.Name,
                AverageRating = Math.Round(g.Average(f => f.Rating), 2),
                TotalResponses = g.Count()
            }).ToListAsync();
    }
}
