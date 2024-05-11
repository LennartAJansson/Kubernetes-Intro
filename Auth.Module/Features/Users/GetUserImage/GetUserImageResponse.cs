namespace Auth.Module.Features.Users.GetUserImage;

using Microsoft.AspNetCore.Http;

internal sealed class GetUserImageResponse
{
  public IFormFile Image { get; set; }
}