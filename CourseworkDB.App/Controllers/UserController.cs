using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class UserController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7098/api");
    private readonly HttpClient _client;
    public UserController()
    {
        _client = new HttpClient();
        _client.BaseAddress = baseAddress;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<User> userList = new List<User>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUsers").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            userList = JsonConvert.DeserializeObject<List<User>>(data);
        }
        return View(userList);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(data);
            return View(user);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/User/UpdateUser", user).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(user);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(data);
            return View(user);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetUserById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            User user = JsonConvert.DeserializeObject<User>(data);
            return View(user);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(User user)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/User/DeleteUser/" + user.UserId).Result;
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
    public IActionResult Create(User user)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/User/CreateUser", user).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(user);
        }
    }
}
