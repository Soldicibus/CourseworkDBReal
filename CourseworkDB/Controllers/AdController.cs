using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdController : Controller
{
    private readonly IAdRepository _adrepos;
    private readonly ILogger<AdController> _logger;
    private readonly IMapper _mapper;
    private readonly DataContext _ctx;
    public AdController(IAdRepository adrepos, ILogger<AdController> logger, IMapper mapper, DataContext ctx)
    {
        _adrepos = adrepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;

    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Ad>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAds()
    {
        try
        {
            var ads = _mapper.Map<List<AdDto>>(await _adrepos.GetAdsAsync());

            if (!ModelState.IsValid) return BadRequest(ads);
            else return Ok(ads);
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
    [HttpGet("{AdId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Ad>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdById(int AdId)
    {
        try
        {
            if (!_adrepos.AdExists(AdId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var ad = await _adrepos.GetAdsByIdAsync(AdId);
            var adDto = _mapper.Map<AdDto>(ad);
            if (!ModelState.IsValid) return BadRequest(adDto);
            if (adDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adDto);
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
    [HttpGet("{AdName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Ad>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdByName(string AdName)
    {
        try
        {
            var ad = await _adrepos.GetAdsByTitleAsync(AdName);
            var adDto = _mapper.Map<AdDto>(ad);
            if (!ModelState.IsValid) return BadRequest(adDto);
            if (adDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adDto);
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
    [HttpGet("{CampaignId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Ad>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdByCampaign(int CampaignId)
    {
        try
        {
            var ad = await _adrepos.GetAdsByCampaignAsync(CampaignId);
            var adDto = _mapper.Map<AdDto>(ad);
            if (!ModelState.IsValid) return BadRequest(adDto);
            if (adDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adDto);
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
    [HttpGet("{TypeId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Ad>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdByType(int TypeId)
    {
        try
        {
            var ad = await _adrepos.GetAdsByTypeAsync(TypeId);
            var adDto = _mapper.Map<AdDto>(ad);
            if (!ModelState.IsValid) return BadRequest(adDto);
            if (adDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(adDto);
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
    public async Task<IActionResult> AddAd(AdCreationDto adDto)
    {
        try
        {
            var adType = await _ctx.AdTypes.FindAsync(adDto.AdTypeId);
            var adCampaign = await _ctx.AdCampaigns.FindAsync(adDto.AdCampaignId);
            if (adType == null || adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var ad = _mapper.Map<Ad>(adDto);
            ad.AdType = adType;
            ad.AdCampaign = adCampaign;
            var createdAd = await _adrepos.CreateAdAsync(ad);

            return CreatedAtAction(nameof(AddAd), adDto);
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
    public async Task<IActionResult> UpdateAd(AdCreationDto adDto)
    {
        try
        {
            var adType = await _ctx.AdTypes.FindAsync(adDto.AdTypeId);
            var adCampaign = await _ctx.AdCampaigns.FindAsync(adDto.AdCampaignId);
            if (adType == null || adCampaign == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var ad = _mapper.Map<Ad>(adDto);
            ad.AdType = adType;
            ad.AdCampaign = adCampaign;
            var existAd = await _adrepos.GetAdsByIdAsync(ad.AdId);
            if (existAd == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _adrepos.UpdateAdAsync(ad);
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAd(int id)
    {
        try
        {
            await _adrepos.DeleteAdAsync(id);
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
