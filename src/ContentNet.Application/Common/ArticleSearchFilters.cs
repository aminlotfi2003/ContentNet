using ContentNet.Domain.Articles;

namespace ContentNet.Application.Common;

public class ArticleSearchFilters
{
    public string? Title { get; init; }
    public int? CategoryId { get; init; }
    public List<int>? TagIds { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public ArticleStatus? Status { get; init; }
}
