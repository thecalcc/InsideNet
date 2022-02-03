using OnePageNet.App.Data.Models;

namespace OnePageNet.App.Services
{
    public interface IDatabaseService
    {
        Task<T> FindByPublicId<T>(string publicId) where T : BaseEntity;
    }
}