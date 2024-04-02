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
    private readonly DataContext _ctx;

    public AdvertisersController(IAdvertisersRepository advertiserrepos, ILogger<AdvertisersController> logger, IMapper mapper, DataContext ctx)
    {
        _advertiserrepos = advertiserrepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;
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
    [HttpGet("{AdvertiserId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Advertiser>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAdvertiserByIdCreation(int AdvertiserId)
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
            var advertiserDto = _mapper.Map<AdvertiserCreationDto>(advertiser);
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
    [HttpPost]
    public async Task<IActionResult> AddAdvertiser(AdvertiserCreationDto advertiserDto)
    {
        try
        {
            var user = await _ctx.Users.FindAsync(advertiserDto.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var advertiser = _mapper.Map<Advertiser>(advertiserDto);
            advertiser.User = user;
            var createdAdvertiser = await _advertiserrepos.AddAdvertiserAsync(advertiser);
            return CreatedAtAction(nameof(AddAdvertiser), createdAdvertiser);

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
    public async Task<IActionResult> UpdateAdvertiser(AdvertiserCreationDto advertiserDto)
    {
        try
        {
            var user = await _ctx.Users.FindAsync(advertiserDto.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var advertiserUpdated = _mapper.Map<Advertiser>(advertiserDto);
            advertiserUpdated.User = user;
            var existAdvertiser = await _advertiserrepos.GetAdvertiserByIdAsync(advertiserUpdated.AdvertiserId);
            if (existAdvertiser == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existAdvertiser.User = advertiserUpdated.User;
            await _advertiserrepos.UpdateAdvertiserAsync(existAdvertiser);
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
    [HttpDelete("{AdvertiserId}")]
    public async Task<IActionResult> DeleteAdvertiser(int AdvertiserId)
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
            var existAdvertiser = await _advertiserrepos.GetAdvertiserByIdAsync(AdvertiserId);
            if (existAdvertiser == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _advertiserrepos.DeleteAdvertisersAsync(AdvertiserId);
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
