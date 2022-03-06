using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationsController : BaseController<RelationEntity, RelationDTO>
    {
        public RelationsController(IDatabaseService<RelationEntity, RelationDTO> databaseService)
            : base(databaseService)
        {
        }
    }
}