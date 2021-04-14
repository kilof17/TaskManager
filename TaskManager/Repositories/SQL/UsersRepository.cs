using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repositories.SQL
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private IMailService _mailService;
        private RoleManager<IdentityRole> _roleManager;

        #region ctor

        public UsersRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMailService mailService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _roleManager = roleManager;
        }

        #endregion ctor

        #region Login

        // Token expiry after 90 days
        public async Task<ApiResponse> LoginAsync(UserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null)
            {
                return new ApiResponse
                {
                    Message = "Wrong login",
                    IsSuccess = false
                };
            }
            var rightPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!rightPassword)
            {
                return new ApiResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false
                };
            }
            var userConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!userConfirmed)
            {
                return new ApiResponse
                {
                    Message = "Please confirm your email adress",
                    IsSuccess = false
                };
            }

            var claims = new List<Claim>
            {
                new Claim("Login", model.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email , user.Email ),
             };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthorizationSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthorizationSettings:ValidIssuer"],
                audience: _configuration["AuthorizationSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiResponse
            {
                Message = tokenString,
                IsSuccess = true
            };
        }

        #endregion Login

        #region Registration

        public async Task<ApiResponse> RegisterUserAsync(UserRegistration user, string appUrl)
        {
            if (user == null)
                throw new NullReferenceException("Register model is empty");
            if (user.Password != user.ConfirmPassword)
                return new ApiResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            var newUser = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Login,
                Email = user.Email
            };

            var creatingResult = await _userManager.CreateAsync(newUser, user.Password);

            if (creatingResult.Succeeded)
            {
                var userRole = await _userManager.AddToRoleAsync(newUser, "User");
                if (!userRole.Succeeded)
                    return new ApiResponse
                    {
                        Message = "User don't create",
                        IsSuccess = false,
                        Errors = creatingResult.Errors.Select(e => e.Description)
                    };

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var tokenWithoutSpecialChrs = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmEmailToken));

                string link = $"{appUrl}/users/confirmemail?userid={newUser.Id}&token={tokenWithoutSpecialChrs}";
                await _mailService.SendEmailAsync(newUser.Email, "Confirm your email adress", $"<h1>Welcome to TaskManager {newUser.UserName} </h1>" +
                                                                  $"<p> Please confirm your email by <a href='{link}'> Click here </a> </p>");

                return new ApiResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true
                };
            }
            return new ApiResponse
            {
                Message = "User don't create",
                IsSuccess = false,
                Errors = creatingResult.Errors.Select(e => e.Description)
            };
        }

        #endregion Registration

        #region Confirm Email Adress

        public async Task<ApiResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ApiResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true
                };
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "Email don't confirmed",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        #endregion Confirm Email Adress
    }
}