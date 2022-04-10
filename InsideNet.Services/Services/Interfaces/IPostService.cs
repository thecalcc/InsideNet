using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.Services.Services.Interfaces
{
    public interface IPostService : IDatabaseService<PostEntity,PostDto>
    {
        Task<List<PostDto>> GetTimeline(string id);
        Task<List<PostDto>> GetPostsForUserById(string posterId);
    }
}
