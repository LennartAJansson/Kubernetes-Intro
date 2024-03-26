namespace BuildVersionsApi.Features.BuildVersions.Increment;

using FastEndpoints;

using FluentValidation;

public sealed class IncrementBuildVersionValidator : Validator<IncrementBuildVersionRequest>
{
  public IncrementBuildVersionValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!")
          .MinimumLength(5)
          .WithMessage("Projectname is too short!");
}