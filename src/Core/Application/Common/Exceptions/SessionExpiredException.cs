using Jpl.MicroService.Shared;
using System.Net;

namespace Jpl.MicroService.Application.Common.Exceptions;

public class SessionExpiredException : CustomException
{
    public SessionExpiredException(string message)
        : base(message, null, HttpStatusCode.Unauthorized, (int)CustomResponseCode.SessionExpired)
    {
    }
}