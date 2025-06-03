using AutoMapper;
using RuhsaProject.DTOs.UserDtos;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.WebUI.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
        }
    }
}
