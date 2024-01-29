using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;

namespace TaskManagmentSystem.Service.Interfaces
{
    public interface IUserEntityProfileService
    {
        public Task<BaseResponse<ClaimsIdentity>> CreateUserEntityProfile(UserEntityProfileViewModel model);
        public Task<BaseResponse<bool>> UpdateUserEntityProfile(int id, UserEntityProfileViewModel model);
        public Task<BaseResponse<UserEntityProfileViewModel>> GetUserEntityProfile(int id);
        public Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
