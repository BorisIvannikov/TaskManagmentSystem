using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.DAL;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.DAL.Repositories;
using TaskManagmentSystem.Models;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ReadMe() { return View(); }
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}