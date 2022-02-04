using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentEntitiesController : BaseController<CommentEntity, CommentDto>
    {
        public CommentEntitiesController(IDatabaseService<CommentEntity> databaseService, IMapper mapper) 
            : base(databaseService, mapper)
        {
        }
    }
}
