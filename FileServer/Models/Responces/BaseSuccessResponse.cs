namespace FileServer.Models.Responces;

public class BaseSuccessResponse
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public BaseSuccessResponse()
    {
        StatusCode = StatusCodes.Status200OK;
        Success = true;
    }
    
}