using Microsoft.EntityFrameworkCore;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public interface IDatabaseService
    {
        Task<T> FindByPublicId<T>(DbSet<T> dbSet, string publicId) where T : BaseEntity;
    }
}