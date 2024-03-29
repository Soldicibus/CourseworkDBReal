using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CourseworkDB.Data.Repositories;
using AutoMapper;
using CourseworkDB.Data.Dto;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userrepos;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userrepos, ILogger<UserController> logger, IMapper mapper)
    {
        _userrepos = userrepos;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = _mapper.Map<List<UserDto>>(await _userrepos.GetUsersAsync());

            if (!ModelState.IsValid) return BadRequest(users);
            else return Ok(users);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpGet("{UserId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUserById(int UserId)
    {
        try
        {
            if (!_userrepos.UserExists(UserId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var user = await _userrepos.GetUserByIdAsync(UserId);
            var userDto = _mapper.Map<UserDto>(user);
            if (!ModelState.IsValid) return BadRequest(userDto);
            if (userDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpGet("{UserName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserByUsername(string UserName)
    {
        try
        {
            var user = await _userrepos.GetUserByUsernameAsync(UserName);
            var userDto = _mapper.Map<UserDto_wo_Id>(user);
            if (!ModelState.IsValid) return BadRequest(userDto);
            if (userDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpGet("{Email}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    public async Task<IActionResult> GetUserByEmail(string Email)
    {
        try
        {
            var user = await _userrepos.GetUserByEmailAsync(Email);
            var userDto = _mapper.Map<UserDto_wo_Id>(user);
            if (!_userrepos.IsValidEmail(Email))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new
                {
                    statusCode = 406,
                    message = "Email written incorrectly, please try again with correct email"
                });
            }

            if (!ModelState.IsValid) return BadRequest(userDto);
            if (userDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpGet("{UserId}/roles")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRolesByUserId(int UserId)
    {
        try
        {
            if (!_userrepos.UserExists(UserId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist (error UserId doesn't exitst)"
                });
            }
            var roles = _mapper.Map<List<RoleDto>>(await _userrepos.GetRolesOfAUserAsync(UserId));
            if (!ModelState.IsValid) return BadRequest(roles);
            if (roles == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto_w_pass userDto)
    {
        try
        {
            var user = _mapper.Map<User>(userDto);
            var createdUser = await _userrepos.CreateUserAsync(user);
            return CreatedAtAction(nameof(AddUser), createdUser);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserDto_w_pass userDto)
    {
        try
        {
            var userUpdated = _mapper.Map<User>(userDto);
            var existUser = await _userrepos.GetUserByIdAsync(userUpdated.UserId);
            if (existUser == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existUser.UserName = userUpdated.UserName;
            existUser.Email = userUpdated.Email;
            existUser.Password = userUpdated.Password;
            await _userrepos.UpdateUserAsync(existUser);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
    [HttpDelete("{UserId}")]
    public async Task<IActionResult> DeleteUser(int UserId)
    {
        try
        {
            if (!_userrepos.UserExists(UserId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existUser = await _userrepos.GetUserByIdAsync(UserId);
            if (existUser == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _userrepos.DeleteUserAsync(UserId);
            return NoContent();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
}
