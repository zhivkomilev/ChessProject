using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.EntityModels.UserModels;

namespace Chess.Users.Services.Infrastructure
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            #region Entity to Model
            CreateMap<User, UserModel>();
            #endregion

            #region Model to Entity
            CreateMap<UserModel, User>();
            #endregion
        }
    }
}
