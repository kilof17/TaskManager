using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DTOs;

namespace TaskManager.Profiles
{
    public class AdminProfiles : Profile
    {
        public AdminProfiles()
        {
            CreateMap<AddRole, IdentityRole>();
        }
    }
}