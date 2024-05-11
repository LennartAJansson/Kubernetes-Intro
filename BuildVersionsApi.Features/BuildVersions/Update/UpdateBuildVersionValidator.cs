namespace BuildVersionsApi.Features.BuildVersions.Update;

using FastEndpoints;

using FluentValidation;

public sealed class UpdateBuildVersionValidator
  : Validator<UpdateBuildVersionRequest>
{
  public UpdateBuildVersionValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!")
          .MinimumLength(5)
          .WithMessage("Projectname is too short!");
}