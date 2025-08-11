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

    public DimensionDto? Dimension1 { get; set; }
    public DimensionDto? Dimension2 { get; set; }
    public DimensionDto? Dimension3 { get; set; }

    public List<ItemAttachmentDto> Attachments { get; set; } = new();
}
