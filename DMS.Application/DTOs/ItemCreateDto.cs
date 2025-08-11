public class ItemCreateDto
{
    public int DealerId { get; set; }
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
    public int? MaterialTypeId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public bool HasTestCertificate { get; set; }

    public int? Dimension1Id { get; set; }
    public int? Dimension2Id { get; set; }
    public int? Dimension3Id { get; set; }
}
