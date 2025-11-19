using ContentNet.Application.Abstractions;
using ContentNet.Application.Common;
using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticlesPaged;

public class GetArticlesPagedQueryHandler
    : IRequestHandler<GetArticlesPagedQuery, PagedResult<ArticleListItemDto>>
{
    private readonly IArticleRepository _articleRepository;

    public GetArticlesPagedQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<PagedResult<ArticleListItemDto>> Handle(
        GetArticlesPagedQuery request,
        CancellationToken cancellationToken)
    {
        var filters = new ArticleSearchFilters
        {
            Title = request.Title,
            CategoryId = request.CategoryId,
            TagIds = request.TagIds,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Status = request.Status
        };

        var result = await _articleRepository.SearchAsync(
            filters,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        var items = result.Items.Select(a => new ArticleListItemDto
        {
            Id = a.Id,
            Title = a.Title,
            Slug = a.Slug,
            CategoryName = a.Category?.Name ?? string.Empty,
            Status = a.Status,
            CreatedAt = a.CreatedAt,
            PublishedAt = a.PublishedAt
        }).ToList();

        return new PagedResult<ArticleListItemDto>(
            items,
            result.TotalCount,
            result.PageNumber,
            result.PageSize);
    }
}
