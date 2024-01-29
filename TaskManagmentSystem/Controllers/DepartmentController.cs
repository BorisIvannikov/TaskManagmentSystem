using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("");
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var res = departmentService.
            return RedirectToAction("");
        }

        [HttpGet]
        public IActionResult Create(Department model)
        {
            return RedirectToAction("");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return RedirectToAction("");
        }
    }
}
