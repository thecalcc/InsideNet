using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseController<PostEntity, PostDto>
{
    public PostsController(IDatabaseService<PostEntity, PostDto> databaseService)
        : base(databaseService)
    {
    }
}