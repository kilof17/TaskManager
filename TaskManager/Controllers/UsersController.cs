using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // POST: /users/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistration user)
        {
            if (ModelState.IsValid)
            {
                var result = await _usersRepository.RegisterUserAsync(user);
                if (result.IsSucces)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest("Model is not valid");
        }

        //POST: /user/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _usersRepository.LoginAsync(model);

                if (loginResult.IsSucces)
                    return Ok(loginResult);

                return BadRequest(loginResult);
            }
            return BadRequest("Model is not valid");
        }
    }
}