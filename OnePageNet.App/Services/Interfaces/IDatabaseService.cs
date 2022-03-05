using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces;

public interface IDatabaseService<T, TG>
    where T : BaseEntity
    where TG : BaseDTO
{
    Task<List<TG>> ToListAsync();
    Task<T> FindById(string id);
    Task<bool> AttachUser(TG dto);
    void Update(TG dto);
    Task SaveChangesAsync();
    Task AddAsync(TG dto);
    bool Remove(T entity);
    bool Exists(string id);
}