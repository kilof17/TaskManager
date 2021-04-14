using AutoMapper;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<ApplicationUser, DisplayUser>();
        }
    }
}