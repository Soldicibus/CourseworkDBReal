using AutoMapper;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPaymentRepository _paymentrepos;
    private readonly ILogger<AdController> _logger;
    private readonly DataContext _ctx;
    public PaymentController(IMapper mapper, IPaymentRepository paymentrepos, ILogger<AdController> logger, DataContext ctx)
    {
        _mapper = mapper;
        _paymentrepos = paymentrepos;
        _logger = logger;
        _ctx = ctx;

    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Payment>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPayments()
    {
        try
        {
            var payments = _mapper.Map<List<PaymentDto>>(await _paymentrepos.GetPaymentsAsync());

            if (!ModelState.IsValid) return BadRequest(payments);
            else return Ok(payments);
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
    [HttpGet("{PaymentId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Payment>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetPaymentById(int PaymentId)
    {
        try
        {
            if (!_paymentrepos.PaymentExists(PaymentId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var paymen = await _paymentrepos.GetPaymentByIdAsync(PaymentId);
            var payment = _mapper.Map<PaymentDto>(paymen);
            if (!ModelState.IsValid) return BadRequest(payment);
            if (payment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(payment);
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<Payment>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPaymentByGroup(int CampaignId)
    {
        try
        {
            var paymen = await _paymentrepos.GetPaymentByGroupAsync(CampaignId);
            var payment = _mapper.Map<PaymentDto>(paymen);
            if (!ModelState.IsValid) return BadRequest(payment);
            if (payment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(payment);
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
    [HttpGet("{Date}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Payment>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPaymentByDate(DateTime Date)
    {
        try
        {
            var paymen = await _paymentrepos.GetPaymentByDateAsync(Date);
            var payment = _mapper.Map<PaymentDto>(paymen);

            if (!ModelState.IsValid) return BadRequest(payment);
            if (payment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(payment);
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<Payment>))]
    [ProducesResponseType(500)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetPaymentDecreasedAmount()
    {
        try
        {
            var payment = _mapper.Map<List<PaymentDto>>(await _paymentrepos.GetPaymentsInDecreasingOrderAsync());

            if (!ModelState.IsValid) return BadRequest(payment);
            if (payment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            else return Ok(payment);
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
    public async Task<IActionResult> AddPayment(PaymentCreationDto paymentDto)
    {
        try
        {
            var adGroups = await _ctx.AdGroups.FindAsync(paymentDto.AdGroupId);
            if (adGroups == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var payment = _mapper.Map<Payment>(paymentDto);
            payment.AdGroup = adGroups;
            var createdPayment = await _paymentrepos.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(AddPayment), createdPayment);

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
    public async Task<IActionResult> UpdatePayment(PaymentCreationDto paymentDto)
    {
        try
        {
            var adGroup = await _ctx.AdGroups.FindAsync(paymentDto.AdGroupId);
            if (adGroup == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            var paymentUpdated = _mapper.Map<Payment>(paymentDto);
            paymentUpdated.AdGroup = adGroup;
            var existPayment = await _paymentrepos.GetPaymentByIdAsync(paymentUpdated.PaymentId);
            if (existPayment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            existPayment.AdGroup = paymentUpdated.AdGroup;
            existPayment.PaymentDate = paymentUpdated.PaymentDate;
            existPayment.PaymentAmount = paymentUpdated.PaymentAmount;
            await _paymentrepos.UpdatePaymentAsync(existPayment);
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
    [HttpDelete("{PaymentId}")]
    public async Task<IActionResult> DeletePayment(int PaymentId)
    {
        try
        {
            if (!_paymentrepos.PaymentExists(PaymentId))
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record doesn't exist"
                });
            }
            var existPayment = await _paymentrepos.GetPaymentByIdAsync(PaymentId);
            if (existPayment == null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = "Record not found"
                });
            }
            await _paymentrepos.DeletePaymentsAsync(PaymentId);
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
