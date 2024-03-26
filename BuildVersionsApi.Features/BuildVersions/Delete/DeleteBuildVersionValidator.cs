namespace BuildVersionsApi.Features.BuildVersions.Delete;

using FastEndpoints;

using FluentValidation;

public sealed class DeleteBuildVersionValidator : Validator<DeleteBuildVersionRequest>
{
  public DeleteBuildVersionValidator() => RuleFor(x => x.Id)
          .GreaterThan(0)
          .WithMessage("Id is required!");
}