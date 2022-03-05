using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Services.Interfaces;

namespace OnePageNet.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController<CommentEntity, CommentDto>
{
    public CommentsController(IDatabaseService<CommentEntity, CommentDto> databaseService, IMapper mapper)
        : base(databaseService, mapper)
    {
    }
}