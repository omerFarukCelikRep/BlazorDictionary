using BlazorDictionary.Api.Domain.Models;
using System.Linq.Expressions;

namespace BlazorDictionary.Api.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    int Add(TEntity entity);
    Task<int> AddAsync(TEntity entity);
    int AddRange(IEnumerable<TEntity> entities);
    Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

    int Update(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);

    int Delete(Guid id);
    int Delete(TEntity entity);
    Task<int> DeleteAsync(Guid id);
    Task<int> DeleteAsync(TEntity entity);
    bool DeleteRange(Expression<Func<TEntity, bool>> predicate);
    Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

    int AddOrUpdate(TEntity entity);
    Task<int> AddOrUpdateAsync(TEntity entity);

    IQueryable<TEntity> AsQueryable();
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetAll(bool tracking = true);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool tracking = true);
    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> GetByIdAsync(Guid id, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true, params Expression<Func<TEntity, object>>[] includes);

    Task BulkDeleteById(IEnumerable<Guid> ids);
    Task BulkDelete(Expression<Func<TEntity, bool>> predicate);
    Task BulkDelete(IEnumerable<TEntity> entities);
    Task BulkUpdate(IEnumerable<TEntity> entities);
    Task BulkAdd(IEnumerable<TEntity> entities);

    int SaveChanges();
    Task<int> SaveChangesAsync();
}