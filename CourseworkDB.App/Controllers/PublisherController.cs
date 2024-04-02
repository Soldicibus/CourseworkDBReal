using CourseworkDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using CourseworkDB.Data.Dto;
using Newtonsoft.Json;

namespace CourseworkDB.App.Controllers;

public class PublisherController : Controller
{
    private readonly HttpClient _client;
    private readonly Uri _baseAddress = new Uri("https://localhost:7098/api");

    public PublisherController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Publisher> publisherList = new List<Publisher>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublishers").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            publisherList = JsonConvert.DeserializeObject<List<Publisher>>(data);
        }
        return View(publisherList);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublisherById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            PublisherCreationDto publisher = JsonConvert.DeserializeObject<PublisherCreationDto>(data);
            return View(publisher);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Edit(PublisherCreationDto publisher)
    {
        HttpResponseMessage response = _client.PutAsJsonAsync(_client.BaseAddress + "/Publishers/UpdatePublisher", publisher).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(publisher);
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublisherById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Publisher publisher = JsonConvert.DeserializeObject<Publisher>(data);
            return View(publisher);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Publishers/GetPublisherById/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            Publisher publisher = JsonConvert.DeserializeObject<Publisher>(data);
            return View(publisher);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Publisher publisher)
    {
        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Publishers/DeletePublisher/" + publisher.PublisherId).Result;
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
    public IActionResult Create(PublisherCreationDto publisher)
    {
        HttpResponseMessage response = _client.PostAsJsonAsync(_client.BaseAddress + "/Publishers/AddPublisher", publisher).Result;
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(publisher);
        }
    }
}
