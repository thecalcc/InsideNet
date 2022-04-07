using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController<CommentEntity, CommentDto>
{
    private ICommentService _IDatabaseService;
    public CommentsController(ICommentService databaseService)
        : base(databaseService)
    {
        _IDatabaseService = databaseService;
    }

    [HttpGet]
    [Route("get-by-id/{id}")]
    public async Task<ActionResult<List<CommentDto>>> GetById(string id) 
    {
        var dtos = await _IDatabaseService.GetAllById(id);
        return Ok(dtos);    
    }
}