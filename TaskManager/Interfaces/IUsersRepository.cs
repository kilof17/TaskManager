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
        Task<UserManagerResponse> RegisterUserAsync(UserRegistration user);

        Task<UserManagerResponse> LoginAsync(UserLogin model);
    }
}