using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdStatusController : Controller
{
    private readonly IAdStatusRepository _adStatusrepos;
    private readonly ILogger<AdStatusController> _logger;

    public AdStatusController(IAdStatusRepository adStatusrepos, ILogger<AdStatusController> logger)
    {
        _adStatusrepos = adStatusrepos;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdStatus>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdStatuss()
    {
        try
        {
            var adStatuses = await _adStatusrepos.GetAdStatusesAsync();

            if (!ModelState.IsValid) return BadRequest(adStatuses);
            else return Ok(adStatuses);
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
    [HttpGet("{AdStatusId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdStatus>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdStatusById(int AdStatusId)
    {
        try
        {
            if (!_adStatusrepos.AdStatusExists(AdStatusId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adStatus = await _adStatusrepos.GetAdStatusAsync(AdStatusId);
            if (!ModelState.IsValid) return BadRequest(adStatus);
            //DoubleCheck If exists
            if (adStatus == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adStatus);
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
    [HttpGet("{AdStatusName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdStatus>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdStatusByAdStatusname(string AdStatusName)
    {
        try
        {
            var adStatus = await _adStatusrepos.GetAdStatusByNameAsync(AdStatusName);
            if (!ModelState.IsValid) return BadRequest(adStatus);
            if (adStatus == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adStatus);
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
