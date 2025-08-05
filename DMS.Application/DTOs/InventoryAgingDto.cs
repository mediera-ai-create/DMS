public class InventoryAgingDto
{
    public string ProductName { get; set; } = string.Empty;
    public string VIN { get; set; } = string.Empty;
    public string DealerName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int DaysInInventory { get; set; }
}
