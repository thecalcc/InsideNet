#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupEntitiesController : BaseController<GroupEntity, GroupDTO>
{
    public GroupEntitiesController(IDatabaseService<GroupEntity, GroupDTO> databaseService, IMapper mapper)
        : base(databaseService, mapper)
    {
    }
}