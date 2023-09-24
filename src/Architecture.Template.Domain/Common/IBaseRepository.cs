﻿using System.Linq.Expressions;

namespace Architecture.Template.Domain.Common;

public interface IBaseRepository<T> : IDisposable where T : BaseAuditableEntity
{
    Task<T> InsertAsync(T item, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> itemCollection, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(IEnumerable<T> itemCollection, CancellationToken cancellationToken = default);
    Task<T> SelectAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}