using System.Data;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Domain.ViewModels;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UserEntityProfileController : Controller
    {
        private readonly IUserEntityProfileService userEntityProfileService;
        public UserEntityProfileController(IUserEntityProfileService userEntityProfileService)
        {
            this.userEntityProfileService = userEntityProfileService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create() 
        {
            return View();
        }
        //[HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Check(UserEntityProfileViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var responce = await userEntityProfileService.CreateUserEntityProfile(model);
                TempData["ConfirmationMessage"] = responce.Description;         
            }
            else
            {
                TempData["ConfirmationMessage"] = "Данные введены не корректно";
            }
            return Redirect("Create");
        }

        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() => View();

        //[HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var responce = await userEntityProfileService.Login(model);
                if(responce.StatusCode != Domain.Enums.StatusCode.OK)
                {
                    TempData["ConfirmationMessage"] = responce.Description;
                    return Redirect("Login");
                }

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new System.Security.Claims.ClaimsPrincipal(responce.Data));

                return Redirect("../Home/Index");
            }
            else
            {
                TempData["ConfirmationMessage"] = "Данные введены не корректно";
            }
            return Redirect("Login");
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("../Home/Index");
        }
    }
}
