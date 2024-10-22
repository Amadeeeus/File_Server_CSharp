using FileServer.Models.Responces;
using FileServer.Models.Views;
using FileServer.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers;


[Route("v1/files")]
[ApiController]

public class FIleController: Controller
{
    private readonly ILogger<FIleController> _logger;
    private readonly IFileService _service;

    public FIleController(ILogger<FIleController> logger, IFileService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("/files/")]
    public async Task<IActionResult> GetAllFilesAsync([FromQuery]string cuid)
    {
        _logger.LogInformation("Controller:Getting all files");
        var result = await _service.GetAllFilesAsync(cuid);
        return Ok(new CustomSucessResponse<List<FileView>>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetFileAsync([FromQuery] string cuid)
    {
        _logger.LogInformation("Controller:Getting file");
        var result = await _service.GetFileAsync(cuid);
        return Ok(new CustomSucessResponse<FileView>(result));
    }

    [HttpPost]

    public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string? password, [FromQuery] int expireDays = 1)
    {
        _logger.LogInformation("Controller:Uploading file");
        await _service.UploadFileAsync(file,password, expireDays);
        return Ok(new BaseSuccessResponse());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFile([FromQuery] string password, [FromQuery] string cuid)
    {
        _logger.LogInformation("Controller:Deleting file");
        await _service.DeleteFileAsync(password, cuid);
        return Ok(new BaseSuccessResponse());
    }
}