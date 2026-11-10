using Microsoft.AspNetCore.Mvc;
using SkillLink.Models;
using System.Diagnostics;

namespace SkillLink.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomeForCustomer()
        {
            return View();
        }
        public IActionResult HomeForServiceProvider()
        {
            return View();
        }
        public IActionResult Requests()
        {
            return View();
        }
        public IActionResult MyBookings()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult MaintenanceServices()
        {
            return View();
        }
        public IActionResult CleaningServices()
        {
            return View();
        }
        public IActionResult ConstructionServices()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
