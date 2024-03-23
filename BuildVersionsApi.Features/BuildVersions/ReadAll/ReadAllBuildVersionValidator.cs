namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using FastEndpoints;

using FluentValidation;

public sealed class ReadAllBuildVersionValidator : Validator<ReadAllBuildVersionRequest>
{
  public ReadAllBuildVersionValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!")
          .MinimumLength(5)
          .WithMessage("Projectname is too short!");
}