using Microsoft.AspNetCore.Mvc;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController<CommentEntity, CommentDto>
{
    public CommentsController(IDatabaseService<CommentEntity, CommentDto> databaseService)
        : base(databaseService)
    {
    }
}