public class DimensionDto
{
    public int Id { get; set; }
    public string Size { get; set; } = string.Empty;
    public decimal? Thickness { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Diameter { get; set; }
    public DateTime CreatedAt { get; set; }
}
