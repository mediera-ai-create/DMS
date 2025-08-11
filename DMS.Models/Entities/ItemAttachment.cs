public class ItemAttachment
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty; // relative url, e.g., /uploads/items/1/img.jpg
    public string FileType { get; set; } = string.Empty; // image/pdf etc
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public Item? Item { get; set; }
}