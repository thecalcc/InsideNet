using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services;

public class DatabaseService<T> : IDatabaseService<T> where T : BaseEntity
{
    private readonly OnePageNetDbContext _dbContext;

    public DatabaseService(OnePageNetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<T>> ToListAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<bool> AttachUser(T entity)
    {   
        return false;
    }

    public async Task<T> FindById(string? Id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == Id);
    }

    public void Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);

        await _dbContext.SaveChangesAsync();
    }

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public bool Exists(string Id)
    {
        return _dbContext.Set<T>().Any(e => e.Id.ToString() == Id);
    }
}