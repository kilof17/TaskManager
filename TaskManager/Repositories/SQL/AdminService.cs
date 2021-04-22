using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories.SQL
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(RoleManager<IdentityRole> roleManager,
                            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Add new role

        public async Task<ApiResponse> AddRoleAsync(IdentityRole identityRole)
        {
            var exsist = await _roleManager.RoleExistsAsync(identityRole.Name);
            if (exsist)
                return new ApiResponse
                {
                    Message = "Role already exist",
                    IsSuccess = false
                };

            var result = await _roleManager.CreateAsync(identityRole);

            if (result != null)
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Role created succesfully!"
                };
            return new ApiResponse
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        #endregion Add new role

        #region Add user to role

        public async Task<ApiResponse> AddUserToRoleAsync(string userId, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return new ApiResponse
                {
                    Message = "Role not exist",
                    IsSuccess = false
                };

            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = $"User {user.UserName} added to Role: {roleName}"
                };
            return new ApiResponse
            {
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        #endregion Add user to role

        #region Get all user roles

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return await _userManager.GetRolesAsync(user);

            return null;
        }

        #endregion Get all user roles

        #region Get role by id

        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        #endregion Get role by id
    }
}