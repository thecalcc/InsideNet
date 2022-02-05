using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Services
{
    public class DatabaseService<T> : IDatabaseService<T> where T : BaseEntity
    {
        private readonly OnePageNetDbContext _dbContext;

        public DatabaseService(OnePageNetDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<bool> AttachUser(string id, T? entity = null)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user?.Id != id) return false;

            _dbContext.Attach(user);
            return true;
        }

        public async Task<T> FindByPublicId(string? publicId)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.ToString() == publicId);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public bool Exists(string publicId)
        {
            return _dbContext.Set<T>().Any(e => e.Id.ToString() == publicId);
        }
    }
}