using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController<CommentEntity, CommentDto>
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
        : base(commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("get-by-id/{id}")]
    public async Task<ActionResult<List<CommentDto>>> GetById(string id)
    {
        try
        {
            var dtos = await _commentService.GetAllById(id);
            return Ok(dtos);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}