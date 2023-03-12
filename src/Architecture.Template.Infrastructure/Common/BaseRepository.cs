using System.Linq.Expressions;
using Architecture.Template.Domain.Common;
using Architecture.Template.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Template.Infrastructure.Common;
public partial class BaseRepository<T> : IBaseRepository<T> where T : AuditableEntity
{
    protected readonly ApplicationDbContext context;
    protected readonly DbSet<T> dataset;
    public BaseRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        dataset = context.Set<T>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">id of the item</param>
    /// <returns>false for not found item and true if completed the delete</returns>
    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await dataset.SingleOrDefaultAsync(predicate, cancellationToken);
        if (result == null)
            return false;

        dataset.Remove(result);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> DeleteRangeAsync(IEnumerable<T> itemCollection, CancellationToken cancellationToken = default)
    {
        dataset.RemoveRange(itemCollection);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await dataset.AnyAsync(predicate, cancellationToken);

    public async Task<T> InsertAsync(T item, CancellationToken cancellationToken)
    {
        await dataset.AddAsync(item, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return item;
    }

    public async Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> item, CancellationToken cancellationToken = default)
    {
        await dataset.AddRangeAsync(item, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await dataset.AsNoTracking()
                     .SingleOrDefaultAsync(predicate, cancellationToken);

    public async Task<IEnumerable<T>> SelectAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await dataset.AsNoTracking()
                           .ToListAsync(cancellationToken)//TODO: ver de retornar IList ou mudar ToList pra outra forma de enemeração
            : await dataset.AsNoTracking()
                           .Where(predicate)
                           .ToListAsync();
    }

    public async Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        context.Update(item);
        await context.SaveChangesAsync(cancellationToken);
        return item;
    }

    #region DisposePattern
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
                context.Dispose();
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}


