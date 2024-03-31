using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public AdController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Ad> adList = new List<Ad>();
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Ad/GetAds").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            adList = JsonConvert.DeserializeObject<List<Ad>>(data);
        }
        return View(adList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Ad/GetAdById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Ad ad = JsonConvert.DeserializeObject<Ad>(data);
            return View(ad);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(Ad ad)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync($"{_client.BaseAddress}/Ad/UpdateAd", ad).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(ad);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Ad/GetAdById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Ad ad = JsonConvert.DeserializeObject<Ad>(data);
            return View(ad);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Ad/GetAdById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Ad ad = JsonConvert.DeserializeObject<Ad>(data);
            return View(ad);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Ad ad)
    {
        HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/Ad/DeleteAd/{ad.AdId}").Result;
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
    public IActionResult Create(Ad ad)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync($"{_client.BaseAddress}/Ad/AddAd", ad).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(ad);
        }
    }
}
