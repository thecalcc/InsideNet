using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

#nullable disable
namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupEntitiesController : BaseController<GroupEntity, GroupDTO>
{
    public GroupEntitiesController(IDatabaseService<GroupEntity, GroupDTO> databaseService)
        : base(databaseService)
    {
    }
}