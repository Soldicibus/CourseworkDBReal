﻿using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserRoleController : Controller
{
    private readonly IUserRoleRepository _userRolerepos;
    private readonly ILogger<UserRoleController> _logger;
    private readonly IMapper _mapper;
    public UserRoleController(IUserRoleRepository userRoleRepository, ILogger<UserRoleController> logger, IMapper mapper)
    {
        _userRolerepos = userRoleRepository;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetUserRoles()
    {
        var userRoles = await _userRolerepos.GetUserRoles();
        return Ok(userRoles);
    }
    [HttpPost]
    public async Task<IActionResult> AddUserRole(int userId, int roleId)
    {
        try
        {
            var createdUserRole = await _userRolerepos.AddUserRoleAsync(userId, roleId);
            var createdUserRoleDto = _mapper.Map<UserRoleDto>(createdUserRole);
            if (createdUserRoleDto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    statusCode = 404,
                    message = "Not Found or duplicate key"
                });
            }
            return CreatedAtAction(nameof(AddUserRole), createdUserRoleDto);
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
    [HttpDelete]
    public async Task<IActionResult> DelUserRole(int userId, int roleId)
    {
        try
        {
            await _userRolerepos.DelUserRoleAsync(userId, roleId);
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