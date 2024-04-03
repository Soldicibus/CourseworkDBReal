using System;
using System.Collections.Generic;
using System.Net.Http;
using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class CompanyController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public CompanyController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Company> companyList = new List<Company>();
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Company/GetCompanies").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            companyList = JsonConvert.DeserializeObject<List<Company>>(data);
        }
        return View(companyList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Company/GetCompanyById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            CompanyCreationDto company = JsonConvert.DeserializeObject<CompanyCreationDto>(data);
            return View(company);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(CompanyCreationDto company)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync($"{_client.BaseAddress}/Company/UpdateCompany", company).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(company);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Company/GetCompanyById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Company company = JsonConvert.DeserializeObject<Company>(data);
            return View(company);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Company/GetCompanyById/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Company company = JsonConvert.DeserializeObject<Company>(data);
            return View(company);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Company company)
    {
        HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/Company/DeleteCompany/{company.CompanyId}").Result;
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
    public IActionResult Create(CompanyCreationDto company)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync($"{_client.BaseAddress}/Company/AddCompany", company).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(company);
        }
    }
    [HttpGet]
    public IActionResult AddAdvertiser()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddAdvertiser(int advertiserId, int companyId) 
    {
        string url = $"{_client.BaseAddress}/Company/AddAdvertiserToCompany?companyId={companyId}&advertiserId={advertiserId}";

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
