namespace BuildVersionsApi.Features.BuildVersions.Read;

using FastEndpoints;

using FluentValidation;

public sealed class ReadBuildVersionValidator : Validator<ReadBuildVersionRequest>
{
  public ReadBuildVersionValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!")
          .MinimumLength(5)
          .WithMessage("Projectname is too short!");
}