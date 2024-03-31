using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdStatusController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri baseAddress = new Uri("https://localhost:7098/api");

    public AdStatusController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<AdStatus> adStatusList = new List<AdStatus>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdStatus/GetAdStatuss").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            adStatusList = JsonConvert.DeserializeObject<List<AdStatus>>(data);
        }
        return View(adStatusList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdStatus/GetAdStatusById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdStatus adStatus = JsonConvert.DeserializeObject<AdStatus>(data);
            return View(adStatus);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(AdStatus adStatus)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/AdStatus/UpdateAdStatus", adStatus).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adStatus);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdStatus/GetAdStatusById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdStatus adStatus = JsonConvert.DeserializeObject<AdStatus>(data);
            return View(adStatus);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdStatus/GetAdStatusById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdStatus adStatus = JsonConvert.DeserializeObject<AdStatus>(data);
            return View(adStatus);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(AdStatus adStatus)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/AdStatus/DeleteAdStatus/" + adStatus.StatusId).Result;
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
    public IActionResult Create(AdStatus adStatus)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/AdStatus/AddAdStatus", adStatus).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adStatus);
        }
    }
}
