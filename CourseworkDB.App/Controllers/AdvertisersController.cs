using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CourseworkDB.Data.Dto;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdvertiserController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri baseAddress = new Uri("https://localhost:7098/api");

    public AdvertiserController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Advertiser> advertiserList = new List<Advertiser>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Advertisers/GetAdvertisers").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            advertiserList = JsonConvert.DeserializeObject<List<Advertiser>>(data);
        }
        return View(advertiserList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Advertisers/GetAdvertiserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Advertiser advertiser = JsonConvert.DeserializeObject<Advertiser>(data);
            return View(advertiser);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(AdvertiserCreationDto advertiser)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/Advertisers/UpdateAdvertiser", advertiser).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(advertiser);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Advertisers/GetAdvertiserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Advertiser advertiser = JsonConvert.DeserializeObject<Advertiser>(data);
            return View(advertiser);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Advertisers/GetAdvertiserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Advertiser advertiser = JsonConvert.DeserializeObject<Advertiser>(data);
            return View(advertiser);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Advertiser advertiser)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Advertisers/DeleteAdvertiser/" + advertiser.AdvertiserId).Result;
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
    public IActionResult Create(AdvertiserCreationDto advertiser)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Advertisers/AddAdvertiser", advertiser).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(advertiser);
        }
    }
}
