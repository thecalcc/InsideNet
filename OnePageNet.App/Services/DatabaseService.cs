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

        public async Task<T> FindByPublicId<T>(string publicId) where T : BaseEntity
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(x => x.PublicId == publicId);
        }
    }
}
