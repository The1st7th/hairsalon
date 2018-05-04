using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairList.Models;

namespace HairList.Controllers
{
    public class ItemsController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
          List<customer> allItems = customer.GetAll();
          return View(allItems);
      }

      [HttpGet("/customers/new")]
      public ActionResult CreateForm()
      {
          List<Employee> allItems = Employee.GetAll();
          return View(allItems);
      }
      [HttpPost("/customers")]
      public ActionResult Create()
      {
        customer newItem = new customer (Request.Form["new-customer"], int.Parse(Request.Form["employee"]));
        newItem.save();
       return RedirectToAction("Index");
      }

     [HttpPost("/employee")]
      public ActionResult CreateEmployee()
      {
        Employee newItem = new Employee (Request.Form["new-employee"]);
        newItem.save();
        List<Employee> allItems = Employee.GetAll();
       return RedirectToAction("Index");
      }
      [HttpGet("/newemployee")]
      public ActionResult Stylist()
      {
          return View();
      }
      [HttpGet("/seeemployees")]
      public ActionResult Employeelist()
      {
          List<Employee> allItems = Employee.GetAll();
          return View(allItems);
      }
      [HttpGet("/employeelist/{id}")]
      public ActionResult EmployeeCustomers(int id)
      {
          List<customer> allItems = Employee.Find(id);
          return View("Index",allItems);
      }
      [HttpGet("/items/delete/{id}")]
      public ActionResult DeleteOne(int id)
      {
          customer.delete(id);
          List<customer> allItems = customer.GetAll();
          return View("index", allItems);
      }

    }
}
