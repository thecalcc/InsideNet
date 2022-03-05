using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class DatabaseService<T, TG> : IDatabaseService<T,TG>
    where T : BaseDTO
    where TG : BaseEntity
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public DatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<T>> ToListAsync()
    {
        return _mapper.Map<List<T>>(await _dbContext.Set<TG>().ToListAsync());
    }

    public virtual async Task<bool> AttachUser(T entity)
    {
        return false;
    }

    public async Task<T> FindById(string? id)
    {
        return _mapper.Map<T>(await _dbContext.Set<TG>().FirstOrDefaultAsync(x => x.Id == id));
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
        await _dbContext.Set<TG>().AddAsync(_mapper.Map<TG>(entity));

        await _dbContext.SaveChangesAsync();
    }

    public void Remove(T entity)
    {
        _dbContext.Set<TG>().Remove(_mapper.Map<TG>(entity));
    }

    public bool Exists(string id)
    {
        return _dbContext.Set<TG>().Any(e => e.Id.ToString() == id);
    }
}