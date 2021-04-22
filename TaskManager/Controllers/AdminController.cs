using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TaskManager.DTOs;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [SwaggerTag("Require Admin role")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IAdminService adminService,
                               IMapper mapper,
                               UserManager<ApplicationUser> userManager,
                               IUsersRepository usersRepository)
        {
            _adminService = adminService;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _userManager = userManager;
        }

        #region Get role by id

        [HttpGet("/Admin/Roles/{id}")]
        [ActionName("GetRoleById")]
        [SwaggerOperation("Get specified role")]
        [SwaggerResponse(200, "Reurns role")]
        [SwaggerResponse(400, "Wrong id")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _adminService.GetRoleByIdAsync(id);
            if (role != null)
                return Ok(role);
            return NotFound();
        }

        #endregion Get role by id

        #region Add new role

        //POST: admin/AddRole
        [HttpPost("AddRole")]
        [SwaggerOperation("Add new role to app")]
        public async Task<IActionResult> AddRole([FromBody] AddRole addRole)
        {
            var role = _mapper.Map<IdentityRole>(addRole);
            var result = await _adminService.AddRoleAsync(role);

            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);

            return BadRequest(result);
        }

        #endregion Add new role

        #region Add user to role

        //POST: /admin/addusertorole
        [HttpPost("/Admin/AddUserToRole")]
        [SwaggerOperation("Add specified user to role")]
        public async Task<IActionResult> AddToRole([FromBody] UserRole userRole)
        {
            var result = await _adminService.AddUserToRoleAsync(userRole.UserId, userRole.RoleName);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #endregion Add user to role

        #region Get all user roles

        //GET: /admi/user/roles/{id}
        [HttpGet("/Admin/User/Roles/{id}")]
        [SwaggerOperation("Return all roles for given user id ")]
        [SwaggerResponse(200, "Return list of user roles")]
        [SwaggerResponse(404, "Wrong id")]
        public async Task<IActionResult> GetRoles(string id)
        {
            var userRoles = await _adminService.GetUserRolesAsync(id);
            if (userRoles != null)
                return Ok(userRoles);

            return NotFound();
        }

        #endregion Get all user roles

        #region Get all user groups

        //GET: /admin/user/groups/{id}
        [HttpGet("/Admin/User/Groups/{id}")]
        [SwaggerOperation("Return all user groups for given user id ")]
        [SwaggerResponse(200, "Return list of goroups")]
        [SwaggerResponse(404, "Wrong id")]
        public async Task<IActionResult> GetGroups(string id)
        {
            var userRoles = await _usersRepository.GetUserGroupsAsync(id);
            if (userRoles != null)
                return Ok(userRoles);

            return NotFound();
        }

        #endregion Get all user groups

        #region Get user by id

        //GET: /admin/user/{id}
        [HttpGet("/Admin/User/{id}")]
        [SwaggerOperation("Returns specified user data")]
        [SwaggerResponse(200, "User data")]
        [SwaggerResponse(404, "Wrong id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return Ok(_mapper.Map<DisplayUser>(user));

            return NotFound();
        }

        #endregion Get user by id
    }
}