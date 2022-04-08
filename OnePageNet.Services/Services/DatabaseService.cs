using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.Data.Data;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.Services.Services;

public class DatabaseService<T, TG> : IDatabaseService<T, TG>
    where T : BaseEntity
    where TG : BaseDTO
{
    protected readonly OnePageNetDbContext _dbContext;
    protected readonly IMapper _mapper;

    public DatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<TG>> ToListAsync()
    {
        return _mapper.Map<List<TG>>(await _dbContext.Set<T>().ToListAsync());
    }

    public virtual Task<List<TG>> GetAllById(string id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> AttachUser(TG dto)
    {
        return false;
    }

    public async Task<TG> FindById(string? id)
    {
        return _mapper.Map<TG>(await _dbContext.Set<T>().FirstAsync(x => x.Id == id));
    }

    public void Update(TG dto)
    {
        _dbContext.Entry(_mapper.Map<T>(dto)).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(TG dto)
    {
        var entity = _mapper.Map<T>(dto);

        await _dbContext.Set<T>().AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        dto.Id = entity.Id;
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await _dbContext.Set<T>().FirstAsync(x => x.Id == id);
        var deleted = _dbContext.Set<T>().Remove(entity);
        if (deleted.State != EntityState.Deleted) return false;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public bool Exists(string id)
    {
        return _dbContext.Set<T>().Any(e => e.Id.ToString() == id);
    }
}