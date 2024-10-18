namespace FileServer.Models.Entities;

public class FileEntity
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
    public string Password { get; set; }
}