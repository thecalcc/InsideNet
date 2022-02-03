using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public interface IDatabaseService
    {
        Task<IEnumerable<T>> ToListAsync<T>() where T : BaseEntity;
        Task<T> FindByPublicId<T>(string publicId) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        Task SaveChangesAsync();
        Task AddAsync<T>(T entity) where T : BaseEntity;
        void Remove<T>(T entity) where T : BaseEntity;
        bool Exists<T>(string publicId) where T : BaseEntity;
    }
}