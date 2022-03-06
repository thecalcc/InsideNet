using AutoMapper;
using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;

namespace OnePageNet.Helpers.AutoMapper;

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
        CreateMap<UserRelationEntity, UserRelationsDto>().ReverseMap();
    }
}