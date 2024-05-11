namespace Auth.Module.Features.Users.GetUserImage;

using System;

internal sealed class GetUserImageRequest
{
  public Guid UserId { get; set; }
}