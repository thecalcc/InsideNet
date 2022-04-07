using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : BaseController<PostEntity, PostDto>
{
    private readonly IPostService _postService;
    public PostsController(IPostService databaseService)
        : base(databaseService)
    {
        _postService = databaseService;
    }

    [Route("get-timeline/{id}")]
    public async Task<ActionResult<List<PostDto>>> GetTimeline([FromRoute] string id)
    {
        var dtos = _postService.GetTimeline(id);
        return Ok(dtos);
    }
}