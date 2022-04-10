using AutoMapper;
using InsideNet.Data.Data.Entities;
using InsideNet.Data.Data.Models;

namespace InsideNet.Helpers.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PostEntity, PostDto>().ReverseMap();
        CreateMap<CommentEntity, CommentDto>().ReverseMap();
        CreateMap<GroupEntity, GroupDto>().ReverseMap();
        CreateMap<MessageEntity, MessageDto>().ReverseMap();
        CreateMap<ReactionEntity, ReactionDto>().ReverseMap();
        CreateMap<RelationEntity, RelationDto>().ReverseMap();
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
        CreateMap<UserRelationEntity, UserRelationDto>().ReverseMap();
        CreateMap<UserGroupEntity, UserGroupDto>().ReverseMap();
    }
}