using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PublishersController : Controller
{
    private readonly IPublisherRepository _publisherrepos;
    private readonly ILogger<PublishersController> _logger;
    private readonly IMapper _mapper;
    private readonly DataContext _ctx;

    public PublishersController(IPublisherRepository publisherrepos, ILogger<PublishersController> logger, IMapper mapper, DataContext ctx)
    {
        _publisherrepos = publisherrepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPublishers()
    {
        try
        {
            var publishers = _mapper.Map<List<PublisherDto>>(await _publisherrepos.GetAllPublishersAsync());

            if (!ModelState.IsValid) return BadRequest(publishers);
            else return Ok(publishers);
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
    [HttpGet("{PublisherId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPublisherById(int PublisherId)
    {
        try
        {
            if (!_publisherrepos.PublisherExist(PublisherId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var publisher = await _publisherrepos.GetPublisherByIdAsync(PublisherId);
            var publisherDto = _mapper.Map<PublisherDto>(publisher);
            if (!ModelState.IsValid) return BadRequest(publisherDto);
            if (publisherDto == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(publisherDto);
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
    [HttpGet("{UserId}/publishers")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPublishersByUserId(int UserId)
    {
        try
        {
            var publishers = _mapper.Map<List<PublisherDto>>(await _publisherrepos.GetPublishersByUserId(UserId));
            if (!ModelState.IsValid) return BadRequest(publishers);
            if (publishers == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(publishers);
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
    public async Task<IActionResult> AddPublisher(PublisherCreationDto publisherDto)
    {
        try
        {
            var user = await _ctx.Users.FindAsync(publisherDto.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var publisher = _mapper.Map<Publisher>(publisherDto);
            publisher.User = user;
            var createdPublisher = await _publisherrepos.AddPublisherAsync(publisher);
            return CreatedAtAction(nameof(AddPublisher), createdPublisher);

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
    public async Task<IActionResult> UpdatePublisher(PublisherCreationDto publisherDto)
    {
        try
        {
            var user = await _ctx.Users.FindAsync(publisherDto.UserId);
            if (user == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var publisherUpdated = _mapper.Map<Publisher>(publisherDto);
            publisherUpdated.User = user;
            var existPublisher = await _publisherrepos.GetPublisherByIdAsync(publisherUpdated.PublisherId);
            if (existPublisher == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existPublisher.WebsiteURL = publisherUpdated.WebsiteURL;
            existPublisher.User = publisherUpdated.User;
            await _publisherrepos.UpdatePublisherAsync(existPublisher);
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
    [HttpDelete("{PublisherId}")]
    public async Task<IActionResult> DeletePublisher(int PublisherId)
    {
        try
        {
            if (!_publisherrepos.PublisherExist(PublisherId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existPublisher = await _publisherrepos.GetPublisherByIdAsync(PublisherId);
            if (existPublisher == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _publisherrepos.DeletePublishersAsync(PublisherId);
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
