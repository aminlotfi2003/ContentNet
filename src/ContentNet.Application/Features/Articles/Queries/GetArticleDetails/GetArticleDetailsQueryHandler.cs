using AutoMapper;
using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using ContentNet.Application.Features.Articles.Dtos;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticleDetails;

public class GetArticleDetailsQueryHandler(IArticleRepository repo, IMapper mapper) : IRequestHandler<GetArticleDetailsQuery, ArticleDto>
{
    private readonly IArticleRepository _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<ArticleDto> Handle(GetArticleDetailsQuery request, CancellationToken cancellationToken)
    {
        var article = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article was not found.");

        return _mapper.Map<ArticleDto>(article);
    }
}
