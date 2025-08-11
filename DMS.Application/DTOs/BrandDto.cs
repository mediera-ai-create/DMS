public class BrandDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string? Address2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string GSTIN { get; set; } = string.Empty;
}
