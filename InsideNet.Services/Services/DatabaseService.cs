using AutoMapper;
using Microsoft.EntityFrameworkCore;
using InsideNet.Data.Data;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.Services.Services;

public class DatabaseService<T, TG> : IDatabaseService<T, TG>
    where T : BaseEntity
    where TG : BaseDto
{
    protected readonly OnePageNetDbContext DbContext;
    protected readonly IMapper Mapper;

    public DatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public async Task<List<TG>> ToListAsync()
    {
        return Mapper.Map<List<TG>>(await DbContext.Set<T>().ToListAsync());
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
        return Mapper.Map<TG>(await DbContext.Set<T>().FirstAsync(x => x.Id == id));
    }

    public void Update(TG dto)
    {
        DbContext.Entry(Mapper.Map<T>(dto)).State = EntityState.Modified;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public async Task AddAsync(TG dto)
    {
        var entity = Mapper.Map<T>(dto);

        await DbContext.Set<T>().AddAsync(entity);

        await DbContext.SaveChangesAsync();

        dto.Id = entity.Id;
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await DbContext.Set<T>().FirstAsync(x => x.Id == id);
        var deleted = DbContext.Set<T>().Remove(entity);
        if (deleted.State != EntityState.Deleted) return false;
        await DbContext.SaveChangesAsync();
        return true;
    }

    public bool Exists(string id)
    {
        return DbContext.Set<T>().Any(e => e.Id.ToString() == id);
    }
}