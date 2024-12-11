using AutoMapper;

using EngSite.Api.Models.Entities;
using EngSite.Api.Models.Forum;
using EngSite.Api.Models.User.GetUserData;
using EngSite.Api.Models.User.Registrate;

namespace EngSite.Api.Web.MapperProfiles
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<RegistrationUserData, User>();
            CreateMap<User, UserProfileData>();
            CreateMap<Forum, ForumMessage>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.AuthorName))
                .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.Date));

            CreateMap<ForumMessage, Forum>()
                .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateTime));
        }
    }
}
