using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using System.Collections.Generic;
using System;

namespace ToDo.Controllers
{
  public class ItemsController : Controller
  {

    [HttpGet("/items")]
    public ActionResult Index()
    {
        // Item newItem = new Item(Request.Query["new-item"]);
        // newItem.Save();
        // List<Item> result = new List<Item>();
        // return View(result);
        List<Item> allItems = Item.GetAll();
        return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/items")]
    public ActionResult Create()
    {
      Item newItem = new Item (Request.Form["new-item"]);
      // newItem.Save();
      List<Item> allItems = Item.GetAll();
      return View("Index", allItems);
    }
    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()
    {
        Item.ClearAll();
        return View();
    }
    [HttpGet("/items/{id}")]
    public ActionResult Details(int id)
    {
        Item item = Item.Find(id);
        return View(item);
    }
  }
}
