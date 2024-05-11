namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using FastEndpoints;

using FluentValidation;

public sealed class ReadBuildVersionByNameValidator
  : Validator<ReadBuildVersionByNameRequest>
{
  public ReadBuildVersionByNameValidator() => RuleFor(x => x.ProjectName)
          .NotEmpty()
          .WithMessage("Projectname is required!");
}