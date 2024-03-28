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

    public PublishersController(IPublisherRepository publisherrepos, ILogger<PublishersController> logger, IMapper mapper)
    {
        _publisherrepos = publisherrepos;
        _logger = logger;
        _mapper = mapper;
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
}
