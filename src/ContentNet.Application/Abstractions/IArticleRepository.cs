using ContentNet.Application.Common;
using ContentNet.Domain.Articles;

namespace ContentNet.Application.Abstractions;

public interface IArticleRepository : IRepository<Article>
{
    Task<PagedResult<Article>> SearchAsync(
        ArticleSearchFilters filters,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}
