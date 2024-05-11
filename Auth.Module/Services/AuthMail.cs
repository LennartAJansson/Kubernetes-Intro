namespace Auth.Module.Services;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

internal sealed class AuthMail(ILogger<AuthMail> logger) : IAuthMail
{
  public Task SendEmailAsync(string email, string subject, string message)
  {
    logger.LogInformation("Sending email to '{email}' with subject '{subject}' and message '{message}'", email, subject, message);
    return Task.CompletedTask;
  }
}