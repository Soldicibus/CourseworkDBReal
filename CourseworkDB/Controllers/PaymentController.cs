using AutoMapper;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseworkDB.Api.Controllers;

public class PaymentController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPaymentRepostory _paymentrepos;
    private readonly ILogger<AdController> _logger;
}
