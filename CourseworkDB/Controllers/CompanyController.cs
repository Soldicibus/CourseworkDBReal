using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController : Controller
{
    private readonly DataContext _ctx;
    private readonly ICompanyRepository _companyrepos;
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyRepository companyrepos, DataContext ctx, ILogger<CompanyController> logger, IMapper mapper)
    {
        _companyrepos = companyrepos;
        _ctx = ctx;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companys = _mapper.Map<List<CompanyDto>>(await _companyrepos.GetCompaniesAsync());

            if (!ModelState.IsValid) return BadRequest(companys);
            else return Ok(companys);
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
    [HttpGet("{CompanyId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetCompanyById(int CompanyId)
    {
        try
        {
            var compan = await _companyrepos.GetCompanyAsync(CompanyId);
            var company = _mapper.Map<CompanyDto>(compan);
            if (!ModelState.IsValid) return BadRequest(company);
            if (company == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(company);
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
    [HttpGet("{CompanyName}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCompanyByCompanyname(string CompanyName)
    {
        try
        {
            var compan = await _companyrepos.GetCompanyByNameAsync(CompanyName);
            var company = _mapper.Map<CompanyDto>(compan);
            if (!ModelState.IsValid) return BadRequest(company);
            if (company == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(company);
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
    [HttpGet("{Email}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    public async Task<IActionResult> GetCompanyByEmail(string Email)
    {
        try
        {
            var compan = await _companyrepos.GetCompanyByEmailAsync(Email);
            var company = _mapper.Map<CompanyDto>(compan);
            if (!_companyrepos.IsValidEmail(Email))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new
                {
                    statusCode = 406,
                    message = "Email written incorrectly, please try again with correct email"
                });
            }

            if (!ModelState.IsValid) return BadRequest(company);
            if (company == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(company);
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
