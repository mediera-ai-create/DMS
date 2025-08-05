public interface IReportService
{
    Task<IEnumerable<SalesReportDto>> GetSalesPerformanceByDealerAsync();
    Task<IEnumerable<InventoryAgingDto>> GetInventoryAgingAsync();
    Task<IEnumerable<CustomerSatisfactionDto>> GetCustomerSatisfactionTrendsAsync();


}
