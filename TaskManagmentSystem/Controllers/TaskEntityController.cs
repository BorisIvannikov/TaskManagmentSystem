using System.Collections.Generic;
using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.ViewModels;
using TaskManagmentSystem.Service.Implementations;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class TaskEntityController : Controller
    {
        private readonly IUserEntityService userEntityService;
        private readonly ITaskEntityService taskEntityService;

        public TaskEntityController(IUserEntityService userEntityService, ITaskEntityService taskEntityService)
        {
            this.userEntityService = userEntityService;
            this.taskEntityService = taskEntityService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var responce = await taskEntityService.GetAllTasksEntity();
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data.ToList());
            }
            if (responce.StatusCode == Domain.Enums.StatusCode.ObjectNotFound)
            {
                return View();
            }
            return RedirectToAction("ErrorView");
        }
        [HttpGet]
        public IActionResult GetSubmittedTask() 
        {
            string? ID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserEntityId").Value;
            var responce = taskEntityService.GetTasksEntityByInitiator(Convert.ToInt32(ID));
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data.ToList());
            }
            if (responce.StatusCode == Domain.Enums.StatusCode.ObjectNotFound)
            {
                return View();
            }
            return RedirectToAction("ErrorView");
        }
        [HttpGet]
        public IActionResult GetReceivedTask()
        {
            string? ID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserEntityId").Value;
            var responce = taskEntityService.GetTasksEntityByRecipient(Convert.ToInt32(ID));
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data.ToList());
            }
            if (responce.StatusCode == Domain.Enums.StatusCode.ObjectNotFound)
            {
                return View();
            }
            return RedirectToAction("ErrorView");
        }
        #region unused
        [HttpGet]
        public async Task<IActionResult> GetTask()
        {
            var responce = await taskEntityService.GetTaskEntity(3);
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data);
            }
            return RedirectToAction("ErrorView");
        }
        #endregion
        //[HttpPost]
        public IActionResult CreateTask()
        {
            int id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserEntityId").Value);
            
            var responce = userEntityService.GetUsersEntityLowPosition(id);
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                IEnumerable<UserEntity> users = responce.Data;
                
                //TempData["usersInDb"] = users;

                ViewBag.Users = users;

                return View();
            }                
            return RedirectToAction("ErrorView");
        }

        //[HttpPut]
        public async Task<IActionResult> Check(TaskEntityViewModel model)
        {
            //ПО какойто не понятной для меня причине TempData["usersInDb"] не работает,  
            //List<UserEntity> aaa = TempData["usersInDb"] as List<UserEntity>;
            //model.Recipient = aaa.FirstOrDefault(x => x.UserEntityId == model.RecipientId);

            //пока буду использовать ещё один запрос в бд, но это не корректно
            //var responceGetUsers = await userEntityService.GetAllUsersEntity();
             
            if (ModelState.IsValid /* && responceGetUsers.StatusCode== Domain.Enums.StatusCode.OK*/)
            {
                //model.Recipient = responceGetUsers.Data.ToList().FirstOrDefault(x => x.UserEntityId == model.RecipientId);

                string? ID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserEntityId").Value;

                var responce = await taskEntityService.CreateTaskEntity(model, Convert.ToInt32(ID));
                if (responce.StatusCode == Domain.Enums.StatusCode.OK)
                {
                    TempData["ConfirmationMessage"] = "Ваше задача успешно создана!";
                }
                else
                {
                    TempData["ConfirmationMessage"] = responce.Description;
                }
                return Redirect("CreateTask");
            }

            TempData["ConfirmationMessage"] = "данные введены не корректно";
            return Redirect("CreateTask");
        }
        
        //[HttpPut]
        public async Task<IActionResult> PerformATask(int id)
        {
            var responce = await taskEntityService.PerformATask(id);
            if (responce.StatusCode != Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("GetReceivedTask", "TaskEntity");
        }
    }
}
