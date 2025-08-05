using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("sales-performance")]
    public async Task<IActionResult> GetSalesPerformance()
    {
        var data = await _reportService.GetSalesPerformanceByDealerAsync();
        return Ok(data);
    }

    [HttpGet("inventory-aging")]
    public async Task<IActionResult> GetInventoryAging()
    {
        var data = await _reportService.GetInventoryAgingAsync();
        return Ok(data);
    }

    [HttpGet("customer-satisfaction")]
    public async Task<IActionResult> GetCustomerSatisfaction()
    {
        var data = await _reportService.GetCustomerSatisfactionTrendsAsync();
        return Ok(data);
    }

}
