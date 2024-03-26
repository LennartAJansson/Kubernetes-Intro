namespace BuildVersionsApi.Features.BuildVersions.Delete;

using MediatR;

public class DeleteBuildVersionRequest : IRequest<DeleteBuildVersionResponse>
{
    public int Id { get; set; }
}