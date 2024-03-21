namespace Jpl.MicroService.Infrastructure.Middleware;
public class SuccessResult<T>
{
    public SuccessResult()
    {
        Message = string.Empty;
        StatusCode = 200;
    }

    public T? Data { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}

public static class SuccessResultHelper<T>
{
    public static SuccessResult<T> Map(T data)
    {
        return new SuccessResult<T>
        {
            Data = data
        };
    }
}