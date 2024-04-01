using System;
using System.Net.Http;
using CourseworkDB.Data.Models;
using CourseworkDB.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class UserRoleController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public UserRoleController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<UserRoleDto> userList = new List<UserRoleDto>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/UserRole/GetUserRoles").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<UserRoleDto>>(data);
        }
        return View(userList);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(UserRoleDto userRole)
    {
        string url = $"{_client.BaseAddress}/UserRole/AddUserRole?userId={userRole.UserId}&roleId={userRole.RoleId}";

        HttpResponseMessage response = _client.PostAsync(url, null).Result;

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(userRole);
        }
    }

    [HttpGet]
    public IActionResult Delete(int id, int id2)
    {
        HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/UserRole/GetUserRole?id={id}&id2={id2}").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            UserRole userRole = JsonConvert.DeserializeObject<UserRole>(data);
            return View(userRole);
        }
        else
        {
            return NotFound();
        }
    }
    [HttpPost]
    public IActionResult Delete(UserRoleDto userRole)
    {
        // Assuming userRole is a DTO with UserId and RoleId properties

        // Construct the URL with query parameters
        string url = $"{_client.BaseAddress}/UserRole/DeleteUserRole?userId={userRole.UserId}&roleId={userRole.RoleId}";

        // Create a DELETE request message
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);

        // Send the request
        HttpResponseMessage response = _client.SendAsync(request).Result;

        // Check if the request was successful
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
