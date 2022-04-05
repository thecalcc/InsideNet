using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

#nullable disable
namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : BaseController<GroupEntity, GroupDTO>
{
    public GroupsController(IDatabaseService<GroupEntity, GroupDTO> databaseService)
        : base(databaseService)
    {
    }
}