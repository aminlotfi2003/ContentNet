using ContentNet.Domain.Common;
using System.Linq.Expressions;

namespace ContentNet.Application.Common.Abstractions.Persistence;

public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update (T entity);
    void Remove(T entity);
}
