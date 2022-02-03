using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController<PostEntity>
    {
        public PostsController(IDatabaseService<PostEntity> databaseService, IMapper mapper) 
            : base(databaseService, mapper)
        {
            
        }
    }
}
