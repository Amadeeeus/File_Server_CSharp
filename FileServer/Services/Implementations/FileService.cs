using System.Xml.Linq;
using AutoMapper;
using FileServer.Extensions;
using FileServer.Models.DTOs;
using FileServer.Models.Views;
using FileServer.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Visus.Cuid;

namespace FileServer.Services.Implementations;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IFileRepository _fileRepository;
    private readonly string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
    private readonly IMapper _mapper;
    private readonly PasswordHasher _passwordHasher;
    
    
    public FileService(ILogger<FileService> logger, IFileRepository fileRepository,  IMapper mapper, PasswordHasher passwordHasher)
    {
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _fileRepository = fileRepository;
    }

    public async Task<List<FileView>> GetAllFilesAsync(string password)
    {
        _logger.LogInformation("Service:Getting all files");
        if (!_passwordHasher.VerifyHashAdmin(password))
        {
            _logger.LogError("Service:GetAllFiles:Invalid password");
            throw new UnauthorizedAccessException("invalid password");
        }
        var entityList = await _fileRepository.GetAllFilesAsync();
        _logger.LogInformation("Service:Getting all files completed");
        var result = _mapper.Map<List<FileView>>(entityList);
        return result;
    }

    public async Task<FileView> GetFileAsync(string cuid)
    {
        _logger.LogInformation("Service:Getting file with name");
        var file = await _fileRepository.GetFileAsync(cuid);
        if (file is null)
        {
            _logger.LogError("Service:GetFileAsync:Invalid password or file not found");
            throw new FileNotFoundException("File not found");
        }
        _logger.LogInformation("Service:GetFileAsync:File found");
        var result = _mapper.Map<FileView>(file);
        return result;
    }

    public async Task UploadFileAsync(IFormFile file, string password, int expireDays)
    {
        var fileInfo = _mapper.Map<FileDTO>(file);
        var fileExtension = Path.GetExtension(fileInfo.FileName);
        fileInfo.Id = new Guid();
        fileInfo.FileName =new Cuid2(10).ToString();
        if (password is not null)
        {
            fileInfo.Password = _passwordHasher.HashString(password);
        }
        fileInfo.ExpireDays = expireDays;
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }
        var currentPath = Path.Combine(filepath, fileInfo.FileName+fileExtension);
        fileInfo.FilePath = currentPath;
        await using (var stream = new FileStream(currentPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        _logger.LogInformation("Service:Uploading file");
        await _fileRepository.UploadFileAsync(fileInfo);
    }

    public async Task DeleteFileAsync(string password, string cuid)
    {
        
        var file = await _fileRepository.GetFileAsync(cuid);
        if (password is not null)
        { 
            password = _passwordHasher.HashString(password);
                                                                       
        }
        var check = _passwordHasher.VerifyHash(file.Password, password);
        if (file is null || check != true)
        {
            throw new FileNotFoundException("Invalid password or file not found");
        }
        _logger.LogInformation("Service:Deleting file with name");
        await _fileRepository.DeleteFileAsync(cuid);
        _logger.LogInformation("Service:Deleted file with name");
    }
}