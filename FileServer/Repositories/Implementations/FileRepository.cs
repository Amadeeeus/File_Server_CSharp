using FileServer.Extensions;
using FileServer.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FileServer.Repositories.Implementations;

public class FileRepository : IFileRepository
{
    private readonly ILogger<FileRepository> _logger;
    private readonly AppDbContext _context;
    
    public FileRepository(ILogger<FileRepository> logger, AppDbContext context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<FileDTO>> GetAllFilesAsync()
    {
        _logger.LogInformation("Repository:GetAllFilesAsync");
        return await _context.Files.ToListAsync();
    }

    public async Task<FileDTO> GetFileAsync(string password)
    {
        _logger.LogInformation("Repository:GetFileAsync}");
        return await _context.Files.FirstOrDefaultAsync(f => f.Password == password);
    }

    public async Task UploadFileAsync(FileDTO dto)
    {
        _logger.LogInformation("Repository:UpdateFileAsync: put file data in database");
        _context.Files.Add(dto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFileAsync(string password)
    {
        _logger.LogInformation("Repository:DeleteFileAsync");
        _context.Files.Remove((await _context.Files.FirstOrDefaultAsync(x => x.Password == password))!);
        await _context.SaveChangesAsync();
    }
}