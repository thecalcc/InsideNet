using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.Services.Services.Interfaces
{
    public interface IPostService : IDatabaseService<PostEntity,PostDto>
    {
        Task<List<PostDto>> GetTimeline(string id);
        Task<List<PostDto>> GetPostsForUserById(string posterId);
    }
}
