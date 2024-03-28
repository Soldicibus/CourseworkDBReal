using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdCampaignsController : Controller
{
    private readonly IAdCampaignsRepository _adCampaignrepos;
    private readonly ILogger<AdCampaignsController> _logger;
    private readonly IMapper _mapper;

    public AdCampaignsController(IAdCampaignsRepository adCampaignrepos, ILogger<AdCampaignsController> logger, IMapper mapper)
    {
        _adCampaignrepos = adCampaignrepos;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdCampaigns()
    {
        try
        {
            var adCampaigns = _mapper.Map<List<AdCampaignDto>>(await _adCampaignrepos.GetAdCampaignsAsync());

            if (!ModelState.IsValid) return BadRequest(adCampaigns);
            else return Ok(adCampaigns);
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
    [HttpGet("{AdCampaignId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdCampaignById(int AdCampaignId)
    {
        try
        {
            if (!_adCampaignrepos.AdCampaignExists(AdCampaignId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adCampaign = await _adCampaignrepos.GetAdCampaignAsync(AdCampaignId);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
    [HttpGet("{AdCampaignName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdCampaignByName(string AdCampaignName)
    {
        try
        {
            var adCampaign = await _adCampaignrepos.GetAdCampaignByNameAsync(AdCampaignName);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
    [HttpGet("{StartDate}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdCampaignByStartDate(DateTime StartDate)
    {
        try
        {
            var adCampaign = await _adCampaignrepos.GetAdCampaignByStartDateAsync(StartDate);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
    [HttpGet("{EndDate}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdCampaignByEndDate(DateTime EndDate)
    {
        try
        {
            var adCampaign = await _adCampaignrepos.GetAdCampaignByEndDateAsync(EndDate);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
    [HttpGet("{CompanyId}/adCampaign")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdCampaignByCompany(int CompanyId)
    {
        try
        {
            var adCampaign = await _adCampaignrepos.GetAdCampaignByCompanyAsync(CompanyId);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
    [HttpGet("{StatusId}/adCampaign")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdCampaignByStatus(int StatusId)
    {
        try
        {
            var adCampaign = await _adCampaignrepos.GetAdCampaignByStatusAsync(StatusId);
            var adCampaignDto = _mapper.Map<AdCampaignDto>(adCampaign);
            if (!ModelState.IsValid) return BadRequest(adCampaignDto);
            if (adCampaignDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaignDto);
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
