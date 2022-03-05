using AutoMapper;
using OnePageNet.App.Data.Entities;
using OnePageNet.App.Data.Models;

namespace OnePageNet.App.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostEntity, PostDto>().ReverseMap();
        CreateMap<CommentEntity, CommentDto>().ReverseMap();
        CreateMap<GroupEntity, GroupDTO>().ReverseMap();
        CreateMap<MessageEntity, MessageDto>().ReverseMap();
        CreateMap<ReactionEntity, ReactionDTO>().ReverseMap();
        CreateMap<RelationEntity, RelationDTO>().ReverseMap();
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}