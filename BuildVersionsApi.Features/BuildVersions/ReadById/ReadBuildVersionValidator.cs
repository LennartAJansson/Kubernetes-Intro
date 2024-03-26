namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using FastEndpoints;

using FluentValidation;

public sealed class ReadBuildVersionByIdValidator : Validator<ReadBuildVersionByIdRequest>
{
  public ReadBuildVersionByIdValidator() => RuleFor(x => x.Id)
          .GreaterThan(0)
          .WithMessage("Id is required!");
}