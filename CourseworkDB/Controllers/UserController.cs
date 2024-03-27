using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CourseworkDB.Data.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CourseworkDB.Data.Dto;
using System.Collections.Generic;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : Controller
{
    private readonly DataContext _ctx;
    private readonly IUserRepository _userrepos;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userrepos, DataContext ctx, ILogger<UserController> logger, IMapper mapper)
    {
        _userrepos = userrepos;
        _ctx = ctx;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
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
}
