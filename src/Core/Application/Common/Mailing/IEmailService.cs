namespace Jpl.MicroService.Application.Common.Mailing;

public interface IEmailService : ITransientService
{
    Task<bool> ResetPassword(string userEmail, string firstName, string lastName, string resetPassCode);
}