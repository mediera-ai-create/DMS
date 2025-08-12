public class ItemDto
{
    public int Id { get; set; }
    public int? BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int? MaterialTypeId { get; set; }
    public string MaterialTypeName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public bool HasTestCertificate { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? Dimension1Value { get; set; }
    public string? Dimension2Value { get; set; }
    public string? Dimension3Value { get; set; }

    public string Attachments { get; set; } = string.Empty;
}
