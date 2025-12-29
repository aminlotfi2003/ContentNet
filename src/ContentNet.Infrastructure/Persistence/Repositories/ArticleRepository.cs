using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Domain.Entities;
using ContentNet.Infrastructure.Context;

namespace ContentNet.Infrastructure.Persistence.Repositories;

public class ArticleRepository : Repository<Article>, IArticleRepository
{
    public ArticleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
