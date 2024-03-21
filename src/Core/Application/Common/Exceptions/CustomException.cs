using System.Net;

namespace Jpl.MicroService.Application.Common.Exceptions;

public class CustomException : Exception
{
    public List<string>? ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }
    public int CustomCode { get; }

    public CustomException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, int customCode = 0)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
        CustomCode = customCode;
    }
}