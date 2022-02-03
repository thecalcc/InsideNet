using AutoMapper;
using OnePageNet.App.Data.Models;
using OnePageNet.App.Data.Models.PostDTOs;

namespace OnePageNet.App.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostEntity, PostDTO>();
            CreateMap<PostDTO, PostEntity>();
        }
    }
}
