using ContentNet.Application.Common;
using ContentNet.Application.DTOs;
using ContentNet.Domain.Articles;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticlesPaged;

public class GetArticlesPagedQuery : IRequest<PagedResult<ArticleListItemDto>>
{
    public string? Title { get; set; }
    public int? CategoryId { get; set; }
    public List<int>? TagIds { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public ArticleStatus? Status { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
