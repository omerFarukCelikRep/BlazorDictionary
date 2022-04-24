using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Api.Domain.Models;
using BlazorDictionary.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorDictionary.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly BlazorDictionaryDbContext _context;
    protected DbSet<TEntity> Table => _context.Set<TEntity>();
    public BaseRepository(BlazorDictionaryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException();
    }
    public virtual int Add(TEntity entity)
    {
        Table.Add(entity);

        return _context.SaveChanges();
    }

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await Table.AddAsync(entity);

        return await _context.SaveChangesAsync();
    }

    public virtual int AddOrUpdate(TEntity entity)
    {
        if (!Table.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
        {
            _context.Update(entity);
        }

        return _context.SaveChanges();
    }

    public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
    {
        if (!Table.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
        {
            _context.Update(entity);
        }

        return await  _context.SaveChangesAsync();
    }

    public virtual int AddRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            return 0;
        }

        Table.AddRange(entities);

        return _context.SaveChanges();
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            return 0;
        }

        await Table.AddRangeAsync(entities);

        return await _context.SaveChangesAsync();
    }

    public virtual IQueryable<TEntity> AsQueryable()
    {
        return Table.AsQueryable();
    }

    public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            await Task.CompletedTask;
            return;
        }

        await Table.AddRangeAsync(entities);

        await _context.SaveChangesAsync();
    }

    public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public virtual Task BulkDelete(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public virtual async Task BulkDeleteById(IEnumerable<Guid> ids)
    {
        if (!ids.Any())
        {
            await Task.CompletedTask;
            return;
        }

        _context.RemoveRange(Table.Where(x => ids.Contains(x.Id)));

        await _context.SaveChangesAsync();
    }

    public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public virtual int Delete(Guid id)
    {
        var entity = Table.Find(id);

        return Delete(entity);
    }

    public virtual async Task<int> DeleteAsync(Guid id)
    {
        var entity = await Table.FindAsync(id);

        return await DeleteAsync(entity);
    }

    public int Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            Table.Attach(entity);
        }

        Table.Remove(entity);

        return _context.SaveChanges();
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            Table.Attach(entity);
        }

        Table.Remove(entity);

        return await _context.SaveChangesAsync();
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(predicate);

        return _context.SaveChanges() > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        _context.RemoveRange(predicate);

        return await _context.SaveChangesAsync() > 0;
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Table;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Table.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (tracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual Task<List<TEntity>> GetAll(bool tracking = false)
    {
        if (tracking)
        {
            return Table.AsNoTracking().ToListAsync();
        }

        return Table.ToListAsync();
    }

    public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false)
    {
        if (tracking)
        {
            return Table.Where(predicate).AsNoTracking().ToListAsync();
        }

        return Table.Where(predicate).ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool tracking = false)
    {
        IQueryable<TEntity> query = Table;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Table;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        foreach (Expression<Func<TEntity, object>>? include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        TEntity entity = await Table.FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        if (tracking)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        foreach (var include in includes)
        {
            await _context.Entry(entity).Reference(include).LoadAsync();
        }

        return entity;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Table;

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = ApplyIncludes(query, includes);

        if (tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.SingleOrDefaultAsync();
    }

    public virtual int Update(TEntity entity)
    {
        Table.Update(entity);

        return _context.SaveChanges();
    }

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        Table.Update(entity);

        return await _context.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }
}