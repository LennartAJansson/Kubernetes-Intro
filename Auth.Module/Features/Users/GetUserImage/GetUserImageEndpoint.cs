namespace Auth.Module.Features.Users.GetUserImage;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using FastEndpoints;
using FastEndpoints.Security;

internal sealed class GetUserImageEndpoint : Endpoint<GetUserImageRequest, GetUserImageResponse>
{
  public override void Configure()
  {
    Get("/auth/users/image/{userId}");
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    //Policies("AdminPolicy");
    Policies("UserPolicy");
  }

  public override async Task HandleAsync(GetUserImageRequest r, CancellationToken c)
  {
    string prefix = "images/users/";
    string? imageUrl = Directory.GetFiles(prefix, $"{User.ClaimValue(ClaimTypes.NameIdentifier)}*.*").FirstOrDefault();

    await SendFileAsync(new FileInfo(imageUrl!));
  }
}