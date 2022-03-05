using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services.Interfaces;

public interface IDatabaseService<T, TG> 
    where T : BaseDTO
    where TG : BaseEntity
{
    Task<List<T>> ToListAsync();
    Task<T> FindById(string id);
    Task<bool> AttachUser(T dto);
    void Update(T dto);
    Task SaveChangesAsync();
    Task AddAsync(T dto);
    void Remove(T dto);
    bool Exists(string id);
}