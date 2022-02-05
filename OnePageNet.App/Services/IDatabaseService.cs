using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services;

public interface IDatabaseService<T> where T : BaseEntity
{
    Task<IEnumerable<T>> ToListAsync();
    Task<T> FindByPublicId(string publicId);
    Task<bool> AttachUser(T entity);
    void Update(T entity);
    Task SaveChangesAsync();
    Task AddAsync(T entity);
    void Remove(T entity);
    bool Exists(string publicId);
}