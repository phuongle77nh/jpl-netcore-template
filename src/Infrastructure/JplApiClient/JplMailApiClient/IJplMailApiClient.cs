using Jpl.MicroService.Application.Common.Interfaces;

namespace Jpl.MicroService.Infrastructure.JplMailApiClient;

public interface IJplMailApiClient : ITransientService
{
    Task<bool> SendMail(MailRequest request);
}