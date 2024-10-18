using System.Xml.Linq;
using AutoMapper;
using FileServer.Extensions;
using FileServer.Models.DTOs;
using FileServer.Models.Entities;
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
    private readonly HashingString _hashingString;
    
    
    public FileService(ILogger<FileService> logger, IFileRepository fileRepository, PasswordHasher<FileDTO> hasher, IMapper mapper, HashingString hashingString)
    {
        _hashingString = hashingString;
        _mapper = mapper;
        _passwordHasher = hasher;
        _logger = logger;
        _fileRepository = fileRepository;
    }

    public async Task<List<FileView>> GetAllFilesAsync(string password)
    {
        _logger.LogInformation("Service:Getting all files");
        if (!_hashingString.VerifyHash(password))
        {
            _logger.LogError("Service:GetAllFiles:Invalid password");
            throw new UnauthorizedAccessException("invalid password");
        }
        var result = await _fileRepository.GetAllFilesAsync();
        _logger.LogInformation("Service:Getting all files completed");
        return result;
    }

    public async Task<FileView> GetFileAsync(string filename, string password)
    {
        _logger.LogInformation("Service:Getting file with name");
        var file = await _fileRepository.GetFileAsync(filename, password);
        var check = _passwordHasher.VerifyHashedPassword(file, file.Password, password);
        if (check != PasswordVerificationResult.Success)
        {
            _logger.LogError("Service:GetFileAsync:Invalid password");
            throw new UnauthorizedAccessException("invalid password");
        }
        _logger.LogInformation("Service:GetFileAsync:File found");
        return file;
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
        _logger.LogInformation("Service:Uploading file");
    }

    public async Task DeleteFileAsync(string filename, string password)
    {
        _logger.LogInformation("Service:Deleting file with name");
        var file = await _fileRepository.GetFileAsync(filename, password);
        var check = _passwordHasher.VerifyHashedPassword(file, file.Password, password);
        if (check != PasswordVerificationResult.Success)
        {
            _logger.LogError("Service:DeleteFileAsync:Invalid password");
            throw new UnauthorizedAccessException("invalid password");
        }
        await _fileRepository.DeleteFileAsync(filename);
        _logger.LogInformation("Service:Deleted file with name");
    }
    
}