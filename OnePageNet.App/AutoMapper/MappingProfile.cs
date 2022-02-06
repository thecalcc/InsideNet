using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostEntity, PostDto>();
        CreateMap<PostDto, PostEntity>();

        CreateMap<CommentEntity, CommentDto>();
        CreateMap<CommentDto, CommentEntity>();
    }
}