using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IUsersRepository
    {
        Task<ApiResponse> RegisterUserAsync(UserRegistration user, string appUrl);

        Task<ApiResponse> LoginAsync(UserLogin model);

        public Task<ApiResponse> ConfirmEmailAsync(string userId, string token);
    }
}