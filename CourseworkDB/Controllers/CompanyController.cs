using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController : Controller
{
    private readonly ICompanyRepository _companyrepos;
    private readonly ILogger<CompanyController> _logger;
    private readonly IMapper _mapper;
    private readonly DataContext _ctx;

    public CompanyController(ICompanyRepository companyrepos, ILogger<CompanyController> logger, IMapper mapper, DataContext ctx)
    {
        _companyrepos = companyrepos;
        _logger = logger;
        _mapper = mapper;
        _ctx = ctx;
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
            if (!_companyrepos.CompanyExists(CompanyId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
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
    [HttpPost]
    public async Task<IActionResult> AddCompany(CompanyCreationDto companyDto)
    {
        try
        {
            var publisher = await _ctx.Publishers.FindAsync(companyDto.PublisherId);
            if (publisher == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var company = _mapper.Map<Company>(companyDto);
            company.Publisher = publisher;
            var createdCompany = await _companyrepos.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(AddCompany), createdCompany);

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
    public async Task<IActionResult> UpdateCompany(CompanyCreationDto companyDto)
    {
        try
        {
            var publisher = await _ctx.Publishers.FindAsync(companyDto.PublisherId);
            if (publisher == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var companyUpdated = _mapper.Map<Company>(companyDto);
            var existCompany = await _companyrepos.GetCompanyAsync(companyUpdated.CompanyId);
            if (existCompany == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existCompany.CompanyName = companyUpdated.CompanyName;
            existCompany.CompanyEmail = companyUpdated.CompanyEmail;
            existCompany.CompanyPhone = companyUpdated.CompanyPhone;
            existCompany.Publisher = publisher;
            await _companyrepos.UpdateCompanyAsync(existCompany);
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
    [HttpDelete("{CompanyId}")]
    public async Task<IActionResult> DeleteCompany(int CompanyId)
    {
        try
        {
            if (!_companyrepos.CompanyExists(CompanyId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existCompany = await _companyrepos.GetCompanyAsync(CompanyId);
            if (existCompany == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _companyrepos.DeleteCompanyAsync(CompanyId);
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
    public async Task<IActionResult> AddAdvertiserToCompany(int companyId, int advertiserId)
    {
        try
        {
            if (!_companyrepos.CompanyExists(companyId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var company = await _companyrepos.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var addedAdvertiser = await _companyrepos.AddAdvertiserToCompanyAsync(companyId, advertiserId);
            var outp = _mapper.Map<AdvertiserDto>(addedAdvertiser);
            return CreatedAtAction(nameof(AddCompany), outp);
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
