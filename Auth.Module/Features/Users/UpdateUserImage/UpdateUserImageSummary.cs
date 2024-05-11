namespace Auth.Module.Features.Users.UpdateUserImage;

using FastEndpoints;

public sealed class UpdateUserImageSummary : Summary<UpdateUserImageEndpoint>
{
  public UpdateUserImageSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}