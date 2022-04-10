using Microsoft.AspNetCore.Mvc;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;
using InsideNet.Services.Services.Interfaces;

namespace InsideNet.App.Controllers;

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
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("get-all/{posterId}")]
    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetPostsForUserId([FromRoute] string posterId)
    {
        try
        {
            var dtos = await _postService.GetPostsForUserById(posterId);
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}