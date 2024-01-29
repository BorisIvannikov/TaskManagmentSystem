using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;

namespace TaskManagmentSystem.Service.Interfaces
{
    public interface IUserEntityService
    {
        public Task<BaseResponse<IEnumerable<UserEntity>>> GetAllUsersEntity();
        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityByName(string name);
        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityByPosition(Position position);
        public Task<BaseResponse<bool>> CreateUserEntity(UserEntityViewModel userEntity);
        public Task<BaseResponse<bool>> DeleteUserEntity(int Id);
        public Task<BaseResponse<UserEntity>> GetUserEntity(int Id);
        public Task<BaseResponse<UserEntity>> EditUserEntity(int id, UserEntityViewModel model);

        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityLowPosition(int id);
    }
}
