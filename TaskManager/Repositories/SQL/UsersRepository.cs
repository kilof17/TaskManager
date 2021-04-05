using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
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

        public UsersRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Token expiry after 30 days
        /// </summary>
        public async Task<UserManagerResponse> LoginAsync(UserLogin model)
        {
            var user = await _userManager.FindByNameAsync(model.Login);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "Wrong login",
                    IsSucces = false
                };
            }
            var rightPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!rightPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSucces = false
                };
            }
            var claims = new[]
            {
                new Claim("Login", model.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthorizationSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthorizationSettings:ValidIssuer"],
                audience: _configuration["AuthorizationSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenString,
                IsSucces = true
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(UserRegistration user)
        {
            if (user == null)
                throw new NullReferenceException("Register model is empty");
            if (user.Password != user.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSucces = false
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
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSucces = true
                };
            }
            return new UserManagerResponse
            {
                Message = "User don't create",
                IsSucces = false,
                Errors = creatingResult.Errors.Select(e => e.Description)
            };
        }
    }
}