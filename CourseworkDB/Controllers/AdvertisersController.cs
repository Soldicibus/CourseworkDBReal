using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AdvertisersController : Controller
{
    private readonly IAdvertisersRepository _advertiserrepos;
    private readonly ILogger<AdvertisersController> _logger;
    private readonly IMapper _mapper;

    public AdvertisersController(IAdvertisersRepository advertiserrepos, ILogger<AdvertisersController> logger, IMapper mapper)
    {
        _advertiserrepos = advertiserrepos;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Advertiser>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdvertisers()
    {
        try
        {
            var advertisers = _mapper.Map<List<AdvertiserDto>>(await _advertiserrepos.GetAllAdvertisersAsync());

            if (!ModelState.IsValid) return BadRequest(advertisers);
            else return Ok(advertisers);
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
    [HttpGet("{AdvertiserId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Advertiser>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdvertiserById(int AdvertiserId)
    {
        try
        {
            if (!_advertiserrepos.AdvertiserExist(AdvertiserId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var advertiser = await _advertiserrepos.GetAdvertiserByIdAsync(AdvertiserId);
            var advertiserDto = _mapper.Map<AdvertiserDto>(advertiser);
            if (!ModelState.IsValid) return BadRequest(advertiserDto);
            if (advertiserDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(advertiserDto);
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
    [HttpGet("{UserId}/advertisers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Advertiser>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAdvertisersByUserId(int UserId)
    {
        try
        {
            var advertisers = _mapper.Map<List<AdvertiserDto>>(await _advertiserrepos.GetAdvertisersByUserId(UserId));
            if (!ModelState.IsValid) return BadRequest(advertisers);
            if (advertisers == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(advertisers);
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
