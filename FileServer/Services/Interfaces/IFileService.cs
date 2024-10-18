using FileServer.Models.Views;

namespace FileServer.Services.Implementations;

public interface IFileService
{
    Task<List<FileView>> GetAllFilesAsync(string password);
    Task<FileView> GetFileAsync(string filename, string password);
    Task UploadFileAsync(IFormFile file, string password);
    Task DeleteFileAsync(string filename, string password);
}