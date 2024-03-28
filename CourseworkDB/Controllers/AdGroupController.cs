using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdGroupController : Controller
{
    private readonly IAdGroupsRepository _adCampaignrepos;
    private readonly ILogger<AdGroupController> _logger;
    private readonly IMapper _mapper;

    public AdGroupController(IAdGroupsRepository adCampaignrepos, ILogger<AdGroupController> logger, IMapper mapper)
    {
        _adCampaignrepos = adCampaignrepos;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdGroup()
    {
        try
        {
            var adCampaignInit = await _adCampaignrepos.GetAdGroupsAsync();
            var adCampaigns = _mapper.Map<List<AdGroupsDto>>(adCampaignInit);

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
    [HttpGet("{AdGroupId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdGroupById(int AdGroupId)
    {
        try
        {
            if (!_adCampaignrepos.AdGroupExists(AdGroupId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adCampaignInit = await _adCampaignrepos.GetAdGroupAsync(AdGroupId);
            var adCampaign = _mapper.Map<AdGroupsDto>(adCampaignInit);
            if (!ModelState.IsValid) return BadRequest(adCampaign);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaign);
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
    [HttpGet("{AdGroupName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdGroupByName(string AdGroupName)
    {
        try
        {
            var adCampaignInit = await _adCampaignrepos.GetAdGroupByNameAsync(AdGroupName);
            var adCampaign = _mapper.Map<AdGroupsDto>(adCampaignInit);
            if (!ModelState.IsValid) return BadRequest(adCampaign);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaign);
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
    [HttpGet("{CampaignId}/adgroup")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdGroupByCampaign(int CampaignId)
    {
        try
        {
            var adCampaignInit = await _adCampaignrepos.GetAdGroupByCampaignAsync(CampaignId);
            var adCampaign = _mapper.Map<AdGroupsDto>(adCampaignInit);
            if (!ModelState.IsValid) return BadRequest(adCampaign);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaign);
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
    [HttpGet("{Audience}/adgroups")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdGroupByAudience(string Audience)
    {
        try
        {
            var adCampaignInit = await _adCampaignrepos.GetAdGroupByAudienceAsync(Audience);
            var adCampaign = _mapper.Map<AdGroupsDto>(adCampaignInit);
            if (!ModelState.IsValid) return BadRequest(adCampaign);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adCampaign);
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
