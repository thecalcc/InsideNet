#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupEntitiesController : BaseController<GroupEntity, GroupDto>
    {
        public GroupEntitiesController(IDatabaseService<GroupEntity> databaseService, IMapper mapper) 
            : base(databaseService, mapper)
        {
        }
    }
}
