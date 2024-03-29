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
}
