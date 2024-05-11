namespace Auth.Module.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Auth.Module.Configuration;
using Auth.Module.Features;
using Auth.Module.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

internal sealed class AuthService(RoleManager<AuthRole> roleManager, UserManager<AuthUser> userManager, IAuthMail mail, IOptions<JwtSettings> options)
  : IUserService, IRoleService
{
  private readonly IEnumerable<string> initialUser = ["Admin", "SuperAdmin", "User"];
  private readonly JwtSettings jwtSettings = options.Value;

  public async Task<string> RegisterUser(string firstName, string lastName, string email, string phoneNumber, string password, string confirmPassword, string link)
  {
    if (!password.Equals(confirmPassword))
    {
      throw new Exception("Passwords entered is not equal");
    }

    AuthUser? user = await userManager.FindByEmailAsync(email);
    if (user is not null)
    {
      throw new Exception("User already exists");
    }

    user = new()
    {
      Firstname = firstName,
      Lastname = lastName,
      UserName = email,
      Email = email,
      PhoneNumber = phoneNumber,
      //HACK Forced set to true
      PhoneNumberConfirmed = true
    };

    //Implement this functionality? Connect with Google or any other OAuth provider
    //users.AddLoginAsync(user, new UserLoginInfo("loginProvider", "providerKey", "displayName"));

    IdentityResult result = await userManager.CreateAsync(user, password);

    if (!result.Succeeded)
    {
      throw new Exception(result.Errors.First().Description);
    }

    result = await userManager.AddToRoleAsync(user, "User");
    if (!result.Succeeded)
    {
      throw new Exception(result.Errors.First().Description);
    }

    if (userManager.Users.Count() == 1)
    {
      result = await userManager.AddToRolesAsync(user, initialUser.Where(r => !r.Contains("User")));
    }

    string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
    string json = JsonSerializer.Serialize(new AuthUserAcknowledge(email, token));
    string encoded = Base64UrlEncode(json);

    //string url = link.Replace("register", "acknowledge") + $"?token={encoded}";
    string url = link.Replace("register", "acknowledge") + $"/{encoded}";

    if (mail is not null)
    {
      await mail.SendEmailAsync(user.Email!, "Confirm registration", $"Please confirm your registration by <a href='{url}'>clicking here</a>.");
    }

    return !result.Succeeded
      ? throw new Exception(result.Errors.First().Description)
      : url;
  }

  public async Task<AuthUser> AcknowledgeUser(string token)
  {
    string json = Base64UrlDecode(token);
    AuthUserAcknowledge? acknowledge = JsonSerializer.Deserialize<AuthUserAcknowledge>(json);
    if (acknowledge is null)
    {
      throw new Exception("Token not found");
    }

    AuthUser? user = await userManager.FindByEmailAsync(acknowledge.Email);
    if (user is null)
    {
      throw new Exception("User already exists");
    }
    //DECIDE: Create feature to Acknowledge phonenumber as well
    IdentityResult result = await userManager.ConfirmEmailAsync(user, acknowledge.Token);

    return !result.Succeeded
      ? throw new Exception(result.Errors.First().Description)
      : user;
  }

  public async Task<string> ResetPassword(string email, string password, string confirmPassword, string link)
  {
    if (!password.Equals(confirmPassword))
    {
      throw new Exception("Passwords entered is not equal");
    }

    AuthUser? user = await userManager.FindByEmailAsync(email);
    if (user is null)
    {
      throw new Exception("User not found");
    }

    string token = await userManager.GeneratePasswordResetTokenAsync(user);
    string json = JsonSerializer.Serialize(new AuthResetAcknowledge(email, password, token));
    string encoded = Base64UrlEncode(json);

    //string url = link.Replace("reset", "acknowledge") + $"?token={encoded}";
    string url = link.Replace("reset", "acknowledge") + $"/{encoded}";

    if (mail is not null)
    {
      await mail.SendEmailAsync(user.Email!, "Confirm registration", $"Please confirm your registration by <a href='{url}'>clicking here</a>.");
    }

    return url;
  }

  public async Task<AuthUser> AcknowledgePassword(string token)
  {
    string json = Base64UrlDecode(token);
    AuthResetAcknowledge? acknowledge = JsonSerializer.Deserialize<AuthResetAcknowledge>(json);
    if (acknowledge is null)
    {
      throw new Exception("Token not found");
    }

    AuthUser? user = await userManager.FindByEmailAsync(acknowledge.Email);
    if (user is null)
    {
      throw new Exception("User already exists");
    }

    IdentityResult result = await userManager.ResetPasswordAsync(user, acknowledge.Token, acknowledge.Password);

    return !result.Succeeded ? throw new Exception(result.Errors.First().Description) : user;
  }

  public async Task<AuthLogin> LoginUser(string email, string password)
  {
    AuthUser? user = await userManager.FindByEmailAsync(email);
    if (user is null || !user.EmailConfirmed || !user.PhoneNumberConfirmed)
    {
      throw new Exception("User not found or confirmed");
    }

    IEnumerable<string> roles = await userManager.GetRolesAsync(user);

    user.PhoneNumberConfirmed = true;
    user.EmailConfirmed = true;

    bool userSigninResult = await userManager.CheckPasswordAsync(user, password);
    if (!userSigninResult)
    {
      throw new Exception("User not logged in");
    }

    //DECIDE: Implement refresh token
    string jwt = GenerateJwt(user, roles);

    return new AuthLogin(user.Id, user.Firstname!, user.Lastname!, user.UserName, roles, jwt, "");
  }

  public async Task<AuthRole> CreateRole(string roleName)
  {
    AuthRole? role = await roleManager.FindByNameAsync(roleName);
    if (role is not null)
    {
      throw new Exception("Role already exists");
    }

    role = new()
    {
      Name = roleName,
      ConcurrencyStamp = Guid.NewGuid().ToString()
    };

    IdentityResult result = await roleManager.CreateAsync(role);

    return !result.Succeeded
      ? throw new Exception(result.Errors.First().Description)
      : role;
  }

  public async Task<AuthRole> DeleteRole(string roleName)
  {
    AuthRole? role = await roleManager.FindByNameAsync(roleName);
    if (role is null)
    {
      throw new Exception("Role not found");
    }
    IdentityResult result = await roleManager.DeleteAsync(role);

    return !result.Succeeded ? throw new Exception(result.Errors.First().Description) : role;
  }

  public async Task<AuthUser> AssignRole(string email, string roleName)
  {
    AuthUser? user = await userManager.FindByEmailAsync(email);
    if (user is null || !user.EmailConfirmed || !user.PhoneNumberConfirmed)
    {
      throw new Exception("User not found or confirmed");
    }

    AuthRole? role = await roleManager.FindByNameAsync(roleName);
    if (role is null || role.Name is null)
    {
      throw new Exception("Role not found");
    }

    if (await userManager.IsInRoleAsync(user, roleName))
    {
      throw new Exception($"User is already in role {roleName}");
    }

    IdentityResult result = await userManager.AddToRoleAsync(user, role.Name);

    return !result.Succeeded ? throw new Exception(result.Errors.First().Description) : user;
  }

  public async Task<AuthUser> RevokeRole(string email, string roleName)
  {
    AuthUser? user = await userManager.FindByEmailAsync(email);
    if (user is null || !user.EmailConfirmed || !user.PhoneNumberConfirmed)
    {
      throw new Exception("User not found or confirmed");
    }

    AuthRole? role = await roleManager.FindByNameAsync(roleName);

    if (role is null || role.Name is null)
    {
      throw new Exception("Role not found");
    }

    if (await userManager.IsInRoleAsync(user, roleName) is false)
    {
      throw new Exception($"User is not in role {roleName}");
    }

    IdentityResult result = await userManager.RemoveFromRoleAsync(user, role.Name);

    return !result.Succeeded ? throw new Exception(result.Errors.First().Description) : user;
  }

  private string GenerateJwt(AuthUser user, IEnumerable<string> roles)
  {
    List<Claim> claims =
    [
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    ];

    //if (user.Firstname is not null)
    //{
    //  claims.Add(new Claim(ClaimTypes.GivenName, user.Firstname));
    //}

    //if (user.Lastname is not null)
    //{
    //  claims.Add(new Claim(ClaimTypes.Surname, user.Lastname));
    //}

    if (user.UserName is not null)
    {
      claims.Add(new Claim(ClaimTypes.Name, user.UserName));
    }

    if (user.Email is not null)
    {
      claims.Add(new Claim(ClaimTypes.Email, user.Email));
    }

    //if (user.PhoneNumber is not null)
    //{
    //  claims.Add(new Claim(ClaimTypes.HomePhone, user.PhoneNumber));
    //}

    IEnumerable<Claim> roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
    claims.AddRange(roleClaims);

    SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings.Secret));
    SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
    DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.ExpirationInDays));

    JwtSecurityToken token = new(
        issuer: jwtSettings.Issuer,
        audience: jwtSettings.Issuer,
        claims: claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  private static string Base64UrlEncode(string value)
  {
    string convertToBase64String = @Convert.ToBase64String(@Encoding.UTF8.GetBytes(value));
    return Uri.EscapeDataString(convertToBase64String);
  }

  private static string Base64UrlDecode(string value)
  {
    try
    {
      string decodeUrlString = Uri.UnescapeDataString(value.Trim());
      return Encoding.UTF8.GetString(Convert.FromBase64String(decodeUrlString));
    }
    catch (Exception)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(value.Trim()));
    }
  }

  public Task<IEnumerable<AuthRole>> GetRoles()
  {
    IEnumerable<AuthRole> roles = roleManager.Roles.Include(u => u.UserRoles).ThenInclude(ur => ur.User);

    return Task.FromResult(roles);
  }

  public Task<IEnumerable<AuthUser>> GetUsers()
  {
    IEnumerable<AuthUser> users = userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role);

    return Task.FromResult(users);
  }

  public async Task<AuthUser> UpdateUser(string email, string firstname, string lastname, string phoneNumber)
  {
    AuthUser? user = await userManager.FindByNameAsync(email);
    if (user is null)
    {
      throw new Exception("User not found");
    }

    user.Firstname = firstname;
    user.Lastname = lastname;
    user.PhoneNumber = phoneNumber;
    user.EmailConfirmed = true;
    user.PhoneNumberConfirmed = true;
    _ = await userManager.UpdateAsync(user);

    return user;
  }

  public async Task<AuthUser> ChangePassword(string email, string oldPassword, string newPassword, string confirmPassword)
  {
    AuthUser? user = await userManager.FindByNameAsync(email);
    if (user is null)
    {
      throw new Exception("User not found");
    }

    if (!newPassword.Equals(confirmPassword))
    {
      throw new Exception("New passwords entered are not the same");
    }

    bool userSigninResult = await userManager.CheckPasswordAsync(user, oldPassword);
    if (!userSigninResult)
    {
      throw new Exception("Invalid password");
    }

    IdentityResult result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
    return !result.Succeeded ? throw new Exception(result.Errors.First().Description) : user;
  }
}