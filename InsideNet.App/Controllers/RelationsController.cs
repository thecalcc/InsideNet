using Microsoft.AspNetCore.Mvc;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationsController : BaseController<RelationEntity, RelationDto>
    {
        public RelationsController(IDatabaseService<RelationEntity, RelationDto> databaseService)
            : base(databaseService)
        {
        }
    }
}