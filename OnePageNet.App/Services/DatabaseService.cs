﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Services;

public class DatabaseService<T, TG> : IDatabaseService<T, TG>
    where T : BaseEntity
    where TG : BaseDTO
{
    private readonly OnePageNetDbContext _dbContext;
    private readonly IMapper _mapper;

    public DatabaseService(OnePageNetDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<TG>> ToListAsync()
    {
        return _mapper.Map<List<TG>>(await _dbContext.Set<T>().ToListAsync());
    }

    public virtual async Task<bool> AttachUser(TG dto)
    {
        return false;
    }

    public async Task<T> FindById(string? id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
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
        await _dbContext.Set<T>().AddAsync(_mapper.Map<T>(dto));

        await _dbContext.SaveChangesAsync();
    }

    public bool Remove(T entity)
    {
        var deleted = _dbContext.Set<T>().Remove(entity);
        return deleted.State == EntityState.Deleted;
    }

    public bool Exists(string id)
    {
        return _dbContext.Set<T>().Any(e => e.Id.ToString() == id);
    }
}