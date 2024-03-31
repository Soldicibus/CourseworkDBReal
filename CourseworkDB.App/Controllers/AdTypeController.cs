using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdTypeController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri baseAddress = new Uri("https://localhost:7098/api");

    public AdTypeController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<AdType> adTypeList = new List<AdType>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdType/GetAdTypes").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            adTypeList = JsonConvert.DeserializeObject<List<AdType>>(data);
        }
        return View(adTypeList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdType/GetAdTypeById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdType adType = JsonConvert.DeserializeObject<AdType>(data);
            return View(adType);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(AdType adType)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/AdType/UpdateAdType", adType).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adType);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdType/GetAdTypeById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdType adType = JsonConvert.DeserializeObject<AdType>(data);
            return View(adType);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/AdType/GetAdTypeById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdType adType = JsonConvert.DeserializeObject<AdType>(data);
            return View(adType);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(AdType adType)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/AdType/DeleteAdType/" + adType.TypeId).Result;
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
    public IActionResult Create(AdType adType)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/AdType/AddAdType", adType).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adType);
        }
    }
}
