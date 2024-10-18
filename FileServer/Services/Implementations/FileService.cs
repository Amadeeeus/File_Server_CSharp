using System.Xml.Linq;
using AutoMapper;
using FileServer.Models.DTOs;
using FileServer.Models.Views;
using Microsoft.AspNetCore.Identity;
using Visus.Cuid;

namespace FileServer.Services.Implementations;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IFileRepository _fileRepository;
    private readonly PasswordHasher<FileDTO> _passwordHasher;
    private readonly string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
    private readonly IMapper _mapper;
    public FileService(ILogger<FileService> logger, IFileRepository fileRepository, PasswordHasher<FileDTO> hasher, IMapper mapper)
    {
        _mapper = mapper;
        _passwordHasher = hasher;
        _logger = logger;
        _fileRepository = fileRepository;
    }

    public async Task<List<FileView>> GetAllFilesAsync(string password)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("File not found");
        }
    }

    public async Task<FileView> GetFileAsync(string filename, string password)
    {
        
    }

    public async Task UploadFileAsync(IFormFile file, string password)
    {
        
        var fileInfo = _mapper.Map<FileDTO>(file);
        fileInfo.Password = _passwordHasher.HashPassword(fileInfo, password);
        fileInfo.Id = new Cuid2().ToString();
        await using (var stream = new FileStream(filepath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    public async Task DeleteFileAsync(string filename, string password)
    {
        
    }
    
}