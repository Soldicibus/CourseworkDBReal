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
    private readonly IAdGroupsRepository _adGrouprepos;
    private readonly ILogger<AdGroupController> _logger;
    private readonly IMapper _mapper;
    private readonly DataContext _ctx;

    public AdGroupController(IAdGroupsRepository adGrouprepos, ILogger<AdGroupController> logger, IMapper mapper, DataContext ctx)
    {
        _adGrouprepos = adGrouprepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdGroup()
    {
        try
        {
            var adCampaigns = await _adGrouprepos.GetAdGroupsAsync();

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
            if (!_adGrouprepos.AdGroupExists(AdGroupId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adCampaignInit = await _adGrouprepos.GetAdGroupAsync(AdGroupId);
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
            var adCampaignInit = await _adGrouprepos.GetAdGroupByNameAsync(AdGroupName);
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
            var adCampaignInit = await _adGrouprepos.GetAdGroupByAudienceAsync(Audience);
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
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdGroup>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdGroupDecreasingBidAmount()
    {
        try
        {
            var adCampaign = _mapper.Map<List<AdGroupsDto>>(await _adGrouprepos.GetAdGroupsInDecreasingOrderAsync());
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
    [HttpPost]
    public async Task<IActionResult> AddAdGroup(AdGroupsDto adGroupsdto)
    {
        try
        {
            var adGroup = _mapper.Map<AdGroup>(adGroupsdto);
            var createdAdGroup = await _adGrouprepos.AddAdGroupAsync(adGroup);
            return CreatedAtAction(nameof(AddAdGroup), createdAdGroup);

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
    public async Task<IActionResult> UpdateAdGroup(AdGroupsDto adGroupDto)
    {
        try
        {
            var adGroupUpdated = _mapper.Map<AdGroup>(adGroupDto);
            var existAdGroup = await _adGrouprepos.GetAdGroupAsync(adGroupUpdated.GroupId);
            if (existAdGroup == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existAdGroup.Audience = adGroupUpdated.Audience;
            existAdGroup.Description = adGroupUpdated.Description;
            existAdGroup.BidAmount = adGroupUpdated.BidAmount;
            existAdGroup.GroupName = adGroupUpdated.GroupName;
            await _adGrouprepos.UpdateAdGroupAsync(existAdGroup);
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
    [HttpDelete("{AdGroupId}")]
    public async Task<IActionResult> DeleteAdGroup(int AdGroupId)
    {
        try
        {
            if (!_adGrouprepos.AdGroupExists(AdGroupId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existAdGroup = await _adGrouprepos.GetAdGroupAsync(AdGroupId);
            if (existAdGroup == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _adGrouprepos.DeleteAdGroupsAsync(AdGroupId);
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
    public async Task<IActionResult> AddAdCampaignToAdGroup(int adGroupId, int adCampaignId)
    {
        try
        {
            if (!_adGrouprepos.AdGroupExists(adGroupId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adGroup = await _adGrouprepos.GetAdGroupAsync(adGroupId);
            if (adGroup == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var addedAdCampaign = await _adGrouprepos.AddAdCampaignToAdGroupAsync(adGroupId, adCampaignId);
            var output = _mapper.Map<AdCampaignDto>(addedAdCampaign);
            return CreatedAtAction(nameof(AddAdGroup), output);
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
