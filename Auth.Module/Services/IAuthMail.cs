namespace Auth.Module.Services;

public interface IAuthMail
{
  Task SendEmailAsync(string email, string subject, string message);
}