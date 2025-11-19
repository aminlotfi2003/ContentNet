using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommand : IRequest<TagDto>
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
}
