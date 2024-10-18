using FileServer.Models.Responces;
using FileServer.Models.Views;
using FileServer.Services.Implementations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers;


[Route("v1/files")]
[ApiController]

public class FIleController: ControllerBase
{
    private readonly ILogger _logger;
    private readonly IFileService _service;

    public FIleController(ILogger<FIleController> logger, IFileService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFilesAsync([FromQuery]string password)
    {
        _logger.LogInformation("Controller:Getting all files");
        var result = await _service.GetAllFilesAsync(password);
        return Ok(new CustomSucessResponse<List<FileView>>(result));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetFileAsync([FromBody] string filename, [FromQuery] string password)
    {
        _logger.LogInformation("Controller:Getting file with name{0}", filename);
        var result = await _service.GetFileAsync(filename, password);
        return Ok(new CustomSucessResponse<FileView>(result));
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string password)
    {
        _logger.LogInformation("Controller:Uploading file");
        await _service.UploadFileAsync(file,password);
        return Ok(new BaseSuccessResponse());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFile(string filename, [FromQuery] string password)
    {
        _logger.LogInformation("Controller:Deleting file {0}", filename);
        await _service.DeleteFileAsync(filename, password);
        return Ok(new BaseSuccessResponse());
    }
}