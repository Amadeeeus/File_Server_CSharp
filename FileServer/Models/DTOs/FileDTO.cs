namespace FileServer.Models.DTOs;

public class FileDTO
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ExpireDays { get; set; }
}