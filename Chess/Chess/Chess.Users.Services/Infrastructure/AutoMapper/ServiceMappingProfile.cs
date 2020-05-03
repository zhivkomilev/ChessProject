using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;

namespace Chess.Users.Services.Infrastructure.AutoMapper
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            #region Entity to Model
            CreateMap<User, UserModel>();
            CreateMap<User, UserDetailsModel>();
            #endregion

            #region Model to Entity
            CreateMap<UserModel, User>();
            CreateMap<UserDetailsModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.LatestUpdateDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            #endregion
        }
    }
}
