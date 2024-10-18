namespace FileServer.Models.Responces;

public class CustomSucessResponse<T>: BaseSuccessResponse
{
    public T Data { get; set; }

    public CustomSucessResponse(T data)
    {
        Data = data;
    }
}