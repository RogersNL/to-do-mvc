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
          Item newItem = new Item(Request.Query["new-item"]);
          newItem.Save();
          List<Item> result = new List<Item>();
          return View(result);
      }

      [HttpGet("/items/new")]
      public ActionResult CreateForm()
      {
          return View();
      }
  }
}
