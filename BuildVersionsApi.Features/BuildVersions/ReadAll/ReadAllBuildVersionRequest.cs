namespace BuildVersionsApi.Features.BuildVersions.ReadAll;
using MediatR;

public class ReadAllBuildVersionRequest : IRequest<IEnumerable<ReadAllBuildVersionResponse>>
{
}