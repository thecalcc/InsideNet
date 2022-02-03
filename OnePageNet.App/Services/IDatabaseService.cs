using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services
{
    public interface IDatabaseService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> ToListAsync();
        Task<T> FindByPublicId(string publicId);
        void Update(T entity);
        Task SaveChangesAsync();
        void AddAsync(T entity);
        void Remove(T entity);
        bool Exists(string publicId);
    }
}