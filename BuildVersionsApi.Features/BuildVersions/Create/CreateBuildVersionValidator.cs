namespace BuildVersionsApi.Features.BuildVersions.Create;

using FastEndpoints;

using FluentValidation;

public sealed class CreateBuildVersionValidator : Validator<CreateBuildVersionRequest>
{
  public CreateBuildVersionValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!")
          .MinimumLength(5)
          .WithMessage("Projectname is too short!");
}