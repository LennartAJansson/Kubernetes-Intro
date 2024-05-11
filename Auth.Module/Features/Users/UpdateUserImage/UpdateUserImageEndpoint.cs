namespace Auth.Module.Features.Users.UpdateUserImage;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using FastEndpoints;
using FastEndpoints.Security;

using Microsoft.AspNetCore.Http;

public sealed class UpdateUserImageEndpoint : Endpoint<UpdateUserImageRequest, UpdateUserImageResponse>
{
  public override void Configure()
  {
    Put("/auth/users/image/update");
    AllowFileUploads(dontAutoBindFormData: true);
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    //Policies("AdminPolicy");
    Policies("UserPolicy");
  }

  public override async Task HandleAsync(UpdateUserImageRequest r, CancellationToken c)
  {
    _ = User.Identity is not null && User.Identity.Name is not null
        ? User.Identity.Name
        : "John Doe";

    if (Files.Count > 0)
    {
      IFormFile file = Files[0];
      string prefix = $"images/users/";

      long timestamp = DateTime.UtcNow.Ticks;
      string imageUrl = $"{prefix}{User.ClaimValue(ClaimTypes.NameIdentifier)}-{timestamp}{Path.GetExtension(file.FileName)}";

      if (!Directory.Exists(prefix))
      {
        _ = Directory.CreateDirectory(prefix);
      }

      foreach (string oldFile in Directory.GetFiles($"{prefix}", $"{User.ClaimValue(ClaimTypes.NameIdentifier)}*.*"))
      {
        File.Delete(oldFile);
      }

      using FileStream filestream = File.Create(imageUrl);
      file.CopyTo(filestream);
      filestream.Flush();

      await SendOkAsync(new UpdateUserImageResponse { Url = imageUrl });

      return;
    }
    await SendNoContentAsync();
  }
}