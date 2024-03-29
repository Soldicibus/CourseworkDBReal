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
    private readonly DataContext _ctx;

    public AdCampaignsController(IAdCampaignsRepository adCampaignrepos, ILogger<AdCampaignsController> logger, IMapper mapper, DataContext ctx)
    {
        _adCampaignrepos = adCampaignrepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
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
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AdCampaign>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdCampaignDecreasingBidAmount()
    {
        try
        {
            var adCampaign = _mapper.Map<List<AdCampaignDto>>(await _adCampaignrepos.GetAdCampaignsInDecreasingOrderAsync());
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
    public async Task<IActionResult> AddAdCampaign(AdCampaignCreationDto adCampaignCreationDto)
    {
        try
        {
            var company = await _ctx.Companies.FindAsync(adCampaignCreationDto.CompanyId);
            if (company == null)
            {
                return NotFound("Publisher or Company not found");
            }

            var adCampaign = _mapper.Map<AdCampaign>(adCampaignCreationDto);
            adCampaign.Company = company;
            var createdAdCampaign = await _adCampaignrepos.CreateAdCampaignAsync(adCampaign);
            return CreatedAtAction(nameof(AddAdCampaign), createdAdCampaign);
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
    public async Task<IActionResult> UpdateAdCampaign(AdCampaignCreationDto adCampaignDto)
    {
        try
        {
            var company = await _ctx.Companies.FindAsync(adCampaignDto.CompanyId);
            if (company == null)
            {
                return NotFound("Publisher or Company not found");
            }

            var existAdCampaign = await _adCampaignrepos.GetAdCampaignAsync(adCampaignDto.CampaignId);
            if (existAdCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existAdCampaign.Company = company;
            existAdCampaign.CampaignName = adCampaignDto.CampaignName;
            existAdCampaign.StartDate = adCampaignDto.StartDate;
            existAdCampaign.EndDate = adCampaignDto.EndDate;
            existAdCampaign.TotalBudget = adCampaignDto.TotalBudget;

            await _adCampaignrepos.UpdateAdCampaignAsync(existAdCampaign);
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
    [HttpDelete("{AdCampaignId}")]
    public async Task<IActionResult> DeleteAdCampaign(int AdCampaignId)
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
            var existAdCampaign = await _adCampaignrepos.GetAdCampaignAsync(AdCampaignId);
            if (existAdCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _adCampaignrepos.DeleteAdCampaignAsync(AdCampaignId);
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
    /*[HttpPut]
    public async Task<IActionResult> AddAdGroupToAdCampaign(int adCampaignId, int adGroupId)
    {
        try
        {
            if (!_adCampaignrepos.AdCampaignExists(adCampaignId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adCampaign = await _adCampaignrepos.GetAdCampaignAsync(adCampaignId);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var addedAdGroup = await _adCampaignrepos.AddAdGroupToAdCampaignAsync(adCampaignId, adGroupId);
            return CreatedAtAction(nameof(AddAdCampaign), addedAdGroup);
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
    }*/
    [HttpPut]
    public async Task<IActionResult> AddAdToAdCampaign(int adCampaignId, int adId)
    {
        try
        {
            if (!_adCampaignrepos.AdCampaignExists(adCampaignId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var adCampaign = await _adCampaignrepos.GetAdCampaignAsync(adCampaignId);
            if (adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var addedAd = await _adCampaignrepos.AddAdToAdCampaignAsync(adCampaignId, adId);
            return CreatedAtAction(nameof(AddAdCampaign), addedAd);
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
