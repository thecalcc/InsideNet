using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public class DatabaseService : IDatabaseService
    {
        public async Task<T> FindByPublicId<T>(DbSet<T> dbSet, string publicId) where T : BaseEntity
        {
            return await dbSet.FirstOrDefaultAsync(x => x.PublicId == publicId);
        }
    }
}
