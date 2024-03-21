using System.Text;
using Jpl.MicroService.Application.Common.Mailing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RazorEngineCore;
using Jpl.MicroService.Infrastructure.JplMailApiClient;
using Jpl.MicroService.Infrastructure.JplApiUrl;

namespace Jpl.MicroService.Infrastructure.Mailing;

public class EmailService : IEmailService
{
    private const string ADMIN_MAIL = "admin@restaff.no";
    private const string TEST_DOMAIN = "mailinator.com";
    private const string ADMIN_SENDER_NAME = "Admin Restaff";
    private IWebHostEnvironment _env { get; set; }

    private readonly JplWebUrlSetting _rsWebUrlSetting;
    private readonly IEmailTemplateService _templateService;
    private readonly IJplMailApiClient _mailApiClient;

    public EmailService(
        IOptions<JplWebUrlSetting> rsWebUrlSetting,
        IEmailTemplateService templateService,
        IWebHostEnvironment env,
        IJplMailApiClient mailApiClient)
    {
        _rsWebUrlSetting = rsWebUrlSetting.Value;
        _templateService = templateService;
        _env = env;
        _mailApiClient = mailApiClient;
    }

    public async Task<bool> ResetPassword(string userEmail, string firstName, string lastName, string resetPassCode)
    {
        string templateKey = "reset_password_v_1_0";
        string tenantId = "restaff";
        string route = _rsWebUrlSetting.ResetPassword.Action;
        var endpointUri = new Uri(string.Concat($"{_rsWebUrlSetting.BaseUrl}/", route));
        var dictionaryParams = new Dictionary<string, string>() { { "passwordResetToken", resetPassCode }, { "email", userEmail } };
        string linkResetPass = QueryHelpers.AddQueryString(endpointUri.ToString(), dictionaryParams);

        string emailTemplate = _templateService.GenerateEmailTemplate("reset_password", new { linkResetPass, firstName });
        string toEmail = userEmail;

        if (_env.IsDevelopment())
        {
            toEmail = userEmail.Replace(userEmail.Substring(userEmail.IndexOf('@') + 1, userEmail.Length - userEmail.IndexOf('@') - 1), TEST_DOMAIN);
        }

        var mailRequest = new MailRequest
        {
            from = new MailAddress
            {
                address = ADMIN_MAIL,
                name = ADMIN_SENDER_NAME
            },
            to = new List<MailAddress>
            {
                new MailAddress
                {
                    address = toEmail,
                    name = $"{lastName} {firstName}"
                }
            },
            html = emailTemplate,
            subject = "Reset your password",
            tenantId = tenantId,
            templateKey = templateKey
        };

        return await _mailApiClient.SendMail(mailRequest);
    }
}