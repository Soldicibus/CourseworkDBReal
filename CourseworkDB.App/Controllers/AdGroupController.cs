using System;
using System.Collections.Generic;
using System.Net.Http;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class AdGroupController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public AdGroupController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<AdGroup> adGroupList = new List<AdGroup>();
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdGroup/GetAdGroup").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            adGroupList = JsonConvert.DeserializeObject<List<AdGroup>>(data);
        }
        return View(adGroupList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdGroup/GetAdGroupById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdGroupsDto adGroup = JsonConvert.DeserializeObject<AdGroupsDto>(data);
            return View(adGroup);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(AdGroupsDto adGroup)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync($"{_client.BaseAddress}/AdGroup/UpdateAdGroup", adGroup).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adGroup);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdGroup/GetAdGroupById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdGroup adGroup = JsonConvert.DeserializeObject<AdGroup>(data);
            return View(adGroup);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/AdGroup/GetAdGroupById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            AdGroup adGroup = JsonConvert.DeserializeObject<AdGroup>(data);
            return View(adGroup);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(AdGroup adGroup)
    {
        HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/AdGroup/DeleteAdGroup/{adGroup.GroupId}").Result;
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
    public IActionResult Create(AdGroupsDto adGroup)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync($"{_client.BaseAddress}/AdGroup/AddAdGroup", adGroup).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(adGroup);
        }
    }
    [HttpGet]
    public IActionResult AddCampaign()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddCampaign(int adCampaignId, int adGroupId)
    {
        string url = $"{_client.BaseAddress}/AdGroup/AddAdCampaignToAdGroup?adGroupId={adCampaignId}&adCampaignId={adGroupId}";

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
