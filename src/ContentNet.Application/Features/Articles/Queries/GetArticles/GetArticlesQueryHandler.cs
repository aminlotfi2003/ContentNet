using AutoMapper;
using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Features.Articles.Dtos;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticles;

public class GetArticlesQueryHandler(IArticleRepository repo, IMapper mapper) : IRequestHandler<GetArticlesQuery, IReadOnlyList<ArticleListItemDto>>
{
    private readonly IArticleRepository _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<ArticleListItemDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken = default)
    {
        var articles = await _repo.ListAsync(cancellationToken);

        return articles.Select(_mapper.Map<ArticleListItemDto>).ToList();
    }
}
