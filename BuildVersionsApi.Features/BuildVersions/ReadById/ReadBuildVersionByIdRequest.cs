namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using MediatR;

public class ReadBuildVersionByIdRequest : IRequest<ReadBuildVersionByIdResponse>
{
    public int Id { get; set; }
}