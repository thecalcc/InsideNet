using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public class DatabaseService<T> : IDatabaseService<T> where T : BaseEntity
    {
        private readonly ApiAuthorizationDbContext<ApplicationUser> _dbContext;

        public DatabaseService(ApiAuthorizationDbContext<ApplicationUser> dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByPublicId(string? publicId)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.PublicId == publicId);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        
        public async void AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }
        
        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public bool Exists(string publicId)        
        {
            return _dbContext.Set<T>().Any(e => e.PublicId == publicId);
        }
    }
}
