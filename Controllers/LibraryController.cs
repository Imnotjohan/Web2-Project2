using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using practica2.Models;

namespace practica2.Controllers;

public class LibraryController : Controller
{
    public List<Books> listBooks;

    public LibraryController()
    {
        var jsonBooks = System.IO.File.ReadAllText("Models/Books.json");
        this.listBooks = JsonConvert.DeserializeObject<List<Books>>(jsonBooks)!;
    }

    public IActionResult Index()
    {
        return View(listBooks);
    }

    public IActionResult Find(string book)
    {
        if (book == null)
        {
            return View("Index", new List<Books>());
        }

        List<Books> FilterBooks = new();

        foreach (var item in this.listBooks)
        {
            if (item.book.ToLower().Contains(book.ToLower()))
            {
                FilterBooks.Add(item);
            }
        }
        return View("Index", FilterBooks);
    }

    public IActionResult Details(string id)
    {
        foreach (var item in this.listBooks)
        {
            if (item.id == id)
                return View(item);
        }
        return View(new Books());
    }
}