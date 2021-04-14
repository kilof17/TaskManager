using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IAdminService
    {
        Task<IdentityRole> GetRoleByIdAsync(string roleId);

        Task<ApiResponse> AddRoleAsync(IdentityRole identityRole);

        Task<ApiResponse> AddUserToRoleAsync(string userId, string roleName);

        Task<IEnumerable<string>> GetAllUsersRolesAsync(string userId);

        public Task<ApplicationUser> GetUserByIdAsync(string id);
    }
}