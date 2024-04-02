using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CourseworkDB.Data.Dto;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class PaymentController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public PaymentController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Payment> paymentList = new List<Payment>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Payment/GetPayments").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            paymentList = JsonConvert.DeserializeObject<List<Payment>>(data);
        }
        return View(paymentList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Payment/GetPaymentById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            PaymentCreationDto payment = JsonConvert.DeserializeObject<PaymentCreationDto>(data);
            return View(payment);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(PaymentCreationDto payment)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/Payment/UpdatePayment", payment).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(payment);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Payment/GetPaymentById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Payment payment = JsonConvert.DeserializeObject<Payment>(data);
            return View(payment);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Payment/GetPaymentById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Payment payment = JsonConvert.DeserializeObject<Payment>(data);
            return View(payment);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Payment payment)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Payment/DeletePayment/" + payment.PaymentId).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(PaymentCreationDto payment)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Payment/AddPayment", payment).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(payment);
        }
    }
}
