using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class UserEntityController : Controller
    {
        private readonly IUserEntityService userEntityService;

        public UserEntityController(IUserEntityService userEntityService)
        {
            this.userEntityService = userEntityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var responce = await userEntityService.GetAllUsersEntity();
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data);
            }
            return RedirectToAction("ErrorView");
        }
        //[HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser(int id) 
        {
            var responce = await userEntityService.DeleteUserEntity(id);
            
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("ErrorView");
        }
        //[HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(UserEntityViewModel user)
        {
            var responce = await userEntityService.CreateUserEntity(user);

            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("ErrorView");
        }
        [HttpGet]
        public IActionResult GetUsersByName(string name) 
        {
            var responce = userEntityService.GetUsersEntityByName(name);
            if(responce.StatusCode != Domain.Enums.StatusCode.OK)
            {
                return RedirectToAction("ErrorView");
            }
            return View(responce.Data);
        }

        [HttpGet]
        public IActionResult GetUsersByPosition(string position)
        {
            var responce = userEntityService.GetUsersEntityByName(position);
            if (responce.StatusCode == Domain.Enums.StatusCode.OK)
            {
                return View(responce.Data);
            }
            return RedirectToAction("ErrorView");
        }

        //[HttpPut]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }
            BaseResponse<UserEntity> response = await userEntityService.GetUserEntity(id);
            if (response.StatusCode ==  Domain.Enums.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("ErrorView");
        }

        //[HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(UserEntityViewModel model)
        {
             if(ModelState.IsValid)
             {
                if(model.Id == 0)
                {
                    await userEntityService.CreateUserEntity(model);
                }
                else
                {
                    await userEntityService.EditUserEntity(model.Id, model);
                }
             }

             return RedirectToAction("GetUsers");
        }
        [HttpGet]
        public IActionResult ErrorView(string errorCode)
        {
            return View();
        }
    }
}
