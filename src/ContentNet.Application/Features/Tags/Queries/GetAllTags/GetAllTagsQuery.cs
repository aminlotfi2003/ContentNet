using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Tags.Queries.GetAllTags;

public class GetAllTagsQuery : IRequest<List<TagDto>>
{
}
