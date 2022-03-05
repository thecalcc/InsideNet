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
        CreateMap<CommentEntity, CommentDto>().ReverseMap();
        CreateMap<CommentDto, CommentEntity>();
        CreateMap<GroupEntity, GroupDTO>().ReverseMap();
        CreateMap<GroupDTO, GroupEntity>();
        CreateMap<MessageEntity, MessageDto>().ReverseMap();
        CreateMap<MessageDto, MessageEntity>();
        CreateMap<ReactionEntity, ReactionDTO>().ReverseMap();
        CreateMap<ReactionDTO, ReactionEntity>();
        CreateMap<RelationEntity, RelationDTO>().ReverseMap();
        CreateMap<RelationDTO, RelationEntity>();
    }
}