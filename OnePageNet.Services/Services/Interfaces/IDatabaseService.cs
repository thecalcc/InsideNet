using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.Services.Services.Interfaces;

public interface IDatabaseService<T, TG>
    where T : BaseEntity
    where TG : BaseDTO
{
    Task<List<TG>> ToListAsync();
    Task<TG> FindById(string id);
    Task<List<TG>> GetAllById(string id);
    Task<bool> AttachUser(TG dto);
    void Update(TG dto);
    Task SaveChangesAsync();
    Task AddAsync(TG dto);
    Task<bool> Remove(string id);
    bool Exists(string id);
}