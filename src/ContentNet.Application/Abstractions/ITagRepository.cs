using ContentNet.Domain.Taxonomy;

namespace ContentNet.Application.Abstractions;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> SlugExistsAsync(string slug, int? excludeId = null, CancellationToken cancellationToken = default);
}
