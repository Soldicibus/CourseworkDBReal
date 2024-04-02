using CourseworkDB.Data.Dto;
using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace CourseworkDB.App.Controllers;

public class RoleController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri baseAddress = new Uri("https://localhost:7098/api");

    public RoleController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Role> roleList = new List<Role>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/GetRoles").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            roleList = JsonConvert.DeserializeObject<List<Role>>(data);
        }
        return View(roleList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/GetRoleById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Role role = JsonConvert.DeserializeObject<Role>(data);
            return View(role);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(Role role)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/Role/UpdateRole", role).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(role);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/GetRoleById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Role role = JsonConvert.DeserializeObject<Role>(data);
            return View(role);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/GetRoleById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Role role = JsonConvert.DeserializeObject<Role>(data);
            return View(role);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Role role)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Role/DeleteRole/" + role.RoleId).Result;
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
    public IActionResult Create(Role role)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Role/AddRole", role).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(role);
        }
    }
    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(int userId, int roleId)
    {
        string url = $"{_client.BaseAddress}/Role/AddUserToRole?roleId={roleId}&userId={userId}";

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
