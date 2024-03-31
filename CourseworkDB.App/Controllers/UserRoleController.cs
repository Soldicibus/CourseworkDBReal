using System;
using System.Net.Http;
using CourseworkDB.Data.Models;
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

    [HttpPost]
    public IActionResult AddUserRole(UserRole userRole)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync($"{_client.BaseAddress}/UserRole/AddUserRole", userRole).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(userRole);
        }
    }

    [HttpPost]
    public IActionResult DeleteUserRole(int id)
    {
        HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/UserRole/DeleteUserRole/{id}").Result;
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
