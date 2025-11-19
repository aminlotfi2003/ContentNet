using ContentNet.Domain.Taxonomy;

namespace ContentNet.Application.Abstractions;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> SlugExistsAsync(string slug, int? excludeId = null, CancellationToken cancellationToken = default);
}
