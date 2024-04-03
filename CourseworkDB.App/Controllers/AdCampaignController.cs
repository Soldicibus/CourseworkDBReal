using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Json;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdCampaignController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public AdCampaignController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<AdCampaign> adCampaignList = new List<AdCampaign>();
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdCampaigns/GetAdCampaigns").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            adCampaignList = JsonConvert.DeserializeObject<List<AdCampaign>>(data);
        }
        return View(adCampaignList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdCampaigns/GetAdCampaignById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdCampaignCreationDto adCampaign = JsonConvert.DeserializeObject<AdCampaignCreationDto>(data);
            return View(adCampaign);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(AdCampaignCreationDto adCampaign)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync($"{_client.BaseAddress}/AdCampaigns/UpdateAdCampaign", adCampaign).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adCampaign);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdCampaigns/GetAdCampaignById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdCampaign adCampaign = JsonConvert.DeserializeObject<AdCampaign>(data);
            return View(adCampaign);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdCampaigns/GetAdCampaignById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdCampaign adCampaign = JsonConvert.DeserializeObject<AdCampaign>(data);
            return View(adCampaign);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(AdCampaign adCampaign)
    {
        HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/AdCampaigns/DeleteAdCampaign/{adCampaign.CampaignId}").Result;
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
    public IActionResult Create(AdCampaignCreationDto adCampaign)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync($"{_client.BaseAddress}/AdCampaigns/AddAdCampaign", adCampaign).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adCampaign);
        }
    }
    [HttpGet]
    public IActionResult AddAd()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddAd(int adCampaign, int adId)
    {
        string url = $"{_client.BaseAddress}/AdCampaigns/AddAdToAdCampaign?adCampaignId={adCampaign}&adId={adId}";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

        HttpResponseMessage response = _client.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return NotFound();
        }
    }
}
