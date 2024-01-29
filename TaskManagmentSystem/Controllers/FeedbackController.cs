using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Service.Interfaces;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;
using System.Security.Claims;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class FeedbackController : Controller
    {

        private readonly IFeedbackService feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(!HttpContext.User.Identity.IsAuthenticated)
            {
                return View();
            }

            string? Name = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            ViewBag.Name = Name;
            return View();
        }

        //[HttpPut]
        public async Task<IActionResult> Check(Feedback feedback) 
        {  
            if (ModelState.IsValid)
            {
                var responce = await feedbackService.CreateFeedback(feedback);
                
                if(responce.StatusCode == Domain.Enums.StatusCode.OK)
                {
                    ViewData["ConfirmationMessage"] = "Ваше сообщение успешно отправлено!";
                }
                else
                {
                    ViewData["ConfirmationMessage"] = responce.Description;
                }
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllFeedbacks()
        {
            var responce = await feedbackService.GetAllFeedbacks();
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
				return View(responce.Data);
			}

			ViewData["ConfirmationMessage"] = responce.Description;
			
            return View("Index");
        }
    }
}
