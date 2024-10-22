using FileServer.Models.DTOs;

namespace FileServer.Repositories.Implementations;

public interface IFileRepository
{
    Task<List<FileDTO>> GetAllFilesAsync();
    Task<FileDTO> GetFileAsync(string cuid);
    Task UploadFileAsync(FileDTO dto);
    Task DeleteFileAsync(string cuid);
}