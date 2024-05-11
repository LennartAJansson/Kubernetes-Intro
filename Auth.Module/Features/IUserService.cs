namespace Auth.Module.Features;

using System.Threading.Tasks;

using Auth.Module.Model;

public interface IUserService
{
  Task<string> RegisterUser(string firstname, string lastname, string email, string phoneNumber, string password, string confirmPassword, string link);

  Task<AuthUser> AcknowledgeUser(string token);

  Task<string> ResetPassword(string email, string password, string confirmPassword, string link);

  Task<AuthUser> AcknowledgePassword(string token);

  Task<AuthLogin> LoginUser(string email, string password);

  Task<IEnumerable<AuthUser>> GetUsers();

  Task<AuthUser> AssignRole(string email, string roleName);

  Task<AuthUser> RevokeRole(string email, string roleName);

  Task<AuthUser> UpdateUser(string email, string firstname, string lastname, string phoneNumber);

  Task<AuthUser> ChangePassword(string email, string oldPassword, string newPassword, string confirmPassword);
}