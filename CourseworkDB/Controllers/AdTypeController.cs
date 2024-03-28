﻿using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdTypeController : Controller
{
    private readonly IAdTypeRepository _adTyperepos;
    private readonly ILogger<AdTypeController> _logger;

    public AdTypeController(IAdTypeRepository adTyperepos, ILogger<AdTypeController> logger)
    {
        _adTyperepos = adTyperepos;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdType>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdTypes()
    {
        try
        {
            var adTypes = await _adTyperepos.GetAdTypesAsync();

            if (!ModelState.IsValid) return BadRequest(adTypes);
            else return Ok(adTypes);
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
    [HttpGet("{AdTypeId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdType>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdTypeById(int AdTypeId)
    {
        try
        {
            if (!_adTyperepos.AdTypeExists(AdTypeId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adType = await _adTyperepos.GetAdTypeAsync(AdTypeId);
            if (!ModelState.IsValid) return BadRequest(adType);
            //DoubleCheck If exists
            if (adType == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adType);
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
    [HttpGet("{AdTypeName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdType>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdTypeByAdTypename(string AdTypeName)
    {
        try
        {
            var adType = await _adTyperepos.GetAdTypeByNameAsync(AdTypeName);
            if (!ModelState.IsValid) return BadRequest(adType);
            if (adType == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adType);
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