using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces;

public interface IDatabaseService<T, TG> 
    where T : BaseDTO
    where TG : BaseEntity
{
    Task<List<T>> ToListAsync();
    Task<T> FindById(string id);
    Task<bool> AttachUser(T entity);
    void Update(T entity);
    Task SaveChangesAsync();
    Task AddAsync(T entity);
    void Remove(T entity);
    bool Exists(string id);
}