using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseController<PostEntity, PostDto>
{
    public PostsController(IDatabaseService<PostEntity> databaseService, IMapper mapper)
        : base(databaseService, mapper)
    {
    }
}