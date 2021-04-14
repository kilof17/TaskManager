using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
        private readonly IMailService _mailService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUsersRepository usersRepository,
                               IMailService mailService,
                               UserManager<ApplicationUser> userManager)
        {
            _usersRepository = usersRepository;
            _mailService = mailService;
            _userManager = userManager;
        }

        #region Registration

        // POST: /users/register
        [HttpPost("Register")]
        [SwaggerOperation("Register new user")]
        [SwaggerResponse(200, "Regirstation success")]
        [SwaggerResponse(400, "Failure, returns errors message")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistration user)
        {
            if (ModelState.IsValid)
            {
                string appUrl = GetApplicationUrl();
                var result = await _usersRepository.RegisterUserAsync(user, appUrl);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest("Model is not valid");
        }

        #endregion Registration

        #region Login

        //POST: /user/login
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Login to app", Description = "Token  returns in key message. Expiry after 90 days")]
        [SwaggerResponse(200, "Login successfully. Token returns in key message")]
        [SwaggerResponse(400, "Wrong login/password or model is not valid. Returns error message ")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _usersRepository.LoginAsync(model);

                if (loginResult.IsSuccess)
                {
                    var email = _userManager.FindByNameAsync(model.Login).Result.Email;
                    await _mailService.SendEmailAsync(email, "New login", "<h1> Somebody login on your account </h1> Login date and time:  " + DateTime.Now);
                    return Ok(loginResult);
                }

                return BadRequest(loginResult);
            }

            return BadRequest("Model is not valid");
        }

        #endregion Login

        #region Confirm Email

        //GET: users/confirmemail?userid&token
        [HttpGet("ConfirmEmail")]
        [SwaggerOperation("Redirect to simply HTML site with confirm email message")]
        [SwaggerResponse(200, "Redirect to confirm site")]
        [SwaggerResponse(400, "Returns errors message")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _usersRepository.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                string appUrl = GetApplicationUrl();
                return Redirect($"{appUrl}/confirmemail.html");
            }
            return BadRequest(result);
        }

        #endregion Confirm Email

        #region Get Application Url

        [NonAction]
        [SwaggerOperation("Metod returns current site link ")]
        private string GetApplicationUrl()
        {
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        }

        #endregion Get Application Url
    }
}