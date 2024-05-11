namespace Auth.Module.Features.Users.UpdateUser;

using System.Threading.Tasks;

using FastEndpoints;

public sealed class UpdateUserEndpoint(IUserService service)
  : Endpoint<UpdateUserRequest, UpdateUserResponse, UpdateUserMapper>
{
  public override void Configure()
  {
    Put("/auth/users/update");
    //AllowAnonymous();
    //Policies("SuperAdminPolicy");
    //Policies("AdminPolicy");
    Policies("UserPolicy");
  }

  public override async Task HandleAsync(UpdateUserRequest r, CancellationToken c)
  {
    Model.AuthUser? user = await service.UpdateUser(r.Emailaddress!, r.Firstname, r.Lastname, r.PhoneNumber);
    if (user is not null)
    {
      await SendAsync(Map.FromEntity(user), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}