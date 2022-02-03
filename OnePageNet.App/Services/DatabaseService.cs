using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApiAuthorizationDbContext<ApplicationUser> dbContext;

        public DatabaseService(ApiAuthorizationDbContext<ApplicationUser> dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> ToListAsync<T>() where T : BaseEntity
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByPublicId<T>(string publicId) where T : BaseEntity
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(x => x.PublicId == publicId);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        //            _context.PostEntities.Add(postEntity);
        public async Task AddAsync<T>(T entity) where T : BaseEntity
        {
            await dbContext.Set<T>().AddAsync(entity);
        }
        //            _context.PostEntities.Remove(postEntity);
        public void Remove<T>(T entity) where T : BaseEntity
        {
            dbContext.Set<T>().Remove(entity);
        }

        public bool Exists<T>(string publicId) where T : BaseEntity        
        {
            return dbContext.Set<T>().Any(e => e.PublicId == publicId);
        }
    }
}
