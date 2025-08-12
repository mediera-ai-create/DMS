public class Item
{
    public int Id { get; set; }
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }            // ItemCategory
    public int? MaterialTypeId { get; set; }        // MaterialType

    public string Name { get; set; } = string.Empty;

    // up to 3 dimension references (nullable)
    public int? Dimension1Id { get; set; }
    public int? Dimension2Id { get; set; }
    public int? Dimension3Id { get; set; }
    public string? Dimension1Value { get; set; }
    public string? Dimension2Value { get; set; }
    public string? Dimension3Value { get; set; }

    public string ItemCategory { get; set; } = string.Empty; // redundant friendly copy if desired
    public string MaterialTypeName { get; set; } = string.Empty; // optional copy
    public string Grade { get; set; } = string.Empty;
    public bool HasTestCertificate { get; set; } = false;
    public string Attachments { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public Brand? Brand { get; set; }
    public ItemCategory? Category { get; set; }
    public MaterialType? MaterialType { get; set; }
    

}