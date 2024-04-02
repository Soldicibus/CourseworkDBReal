using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : Controller
{
    private readonly IRoleRepository _rolerepos;
    private readonly ILogger<RoleController> _logger;
    private readonly IMapper _mapper;

    public RoleController(IRoleRepository rolerepos, ILogger<RoleController> logger, IMapper mapper)
    {
        _rolerepos = rolerepos;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetRoles()
    {
        try
        {
            var roles = _mapper.Map<List<RoleDto>>(await _rolerepos.GetRolesAsync());

            if (!ModelState.IsValid) return BadRequest(roles);
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
    [HttpGet("{RoleId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetRoleById(int RoleId)
    {
        try
        {
            if (!_rolerepos.RoleExists(RoleId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var role = await _rolerepos.GetRoleAsync(RoleId);
            var roleDto = _mapper.Map<RoleDto>(role);
            if (!ModelState.IsValid) return BadRequest(roleDto);
            if (roleDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(roleDto);
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
    [HttpGet("{RoleName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetRoleByRolename(string RoleName)
    {
        try
        {
            var role = await _rolerepos.GetRoleByNameAsync(RoleName);
            var roleDto = _mapper.Map<RoleDto>(role);
            if (!ModelState.IsValid) return BadRequest(roleDto);
            if (roleDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(roleDto);
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
    public async Task<IActionResult> AddRole(RoleDto roleDto)
    {
        try
        {
            var role = _mapper.Map<Role>(roleDto);
            var createdRole = await _rolerepos.CreateRoleAsync(role);
            return CreatedAtAction(nameof(AddRole), createdRole);

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
    public async Task<IActionResult> UpdateRole(RoleDto roleDto)
    {
        try
        {
            var roleUpdated = _mapper.Map<Role>(roleDto);
            var existRole = await _rolerepos.GetRoleAsync(roleUpdated.RoleId);
            if (existRole == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existRole.RoleName = roleUpdated.RoleName;
            await _rolerepos.UpdateRoleAsync(existRole);
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
    [HttpDelete("{RoleId}")]
    public async Task<IActionResult> DeleteRole(int RoleId)
    {
        try
        {
            if (!_rolerepos.RoleExists(RoleId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existRole = await _rolerepos.GetRoleAsync(RoleId);
            if (existRole == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _rolerepos.DeleteRoleAsync(RoleId);
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
    [HttpPost]
    public async Task<IActionResult> AddUserToRole(int roleId, int userId)
    {
        try
        {
            if (!_rolerepos.RoleExists(roleId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var role = await _rolerepos.GetRoleAsync(roleId);
            if (role == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var addedUser = await _rolerepos.AddUserToRoleAsync(roleId, userId);
            var outp = _mapper.Map<UserDto>(addedUser);
            return CreatedAtAction(nameof(AddUserToRole), outp);
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
