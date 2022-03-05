using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationsController : BaseController<RelationEntity, RelationDTO>
    {
        public RelationsController(IDatabaseService<RelationDTO, RelationEntity> databaseService, IMapper mapper)
            : base(databaseService, mapper)
        {
        }
    }
}