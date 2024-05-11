namespace Auth.Module.Features.Users.UpdateUserImage;

using Microsoft.AspNetCore.Http;

public sealed class UpdateUserImageRequest
{
  public IFormFile Image { get; set; }
}