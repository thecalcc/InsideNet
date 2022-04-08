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
    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetTimeline([FromRoute] string id)
    {
        try
        {
            var dtos = await _postService.GetTimeline(id);
            if (dtos.First().Id == null) return BadRequest(id);
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}