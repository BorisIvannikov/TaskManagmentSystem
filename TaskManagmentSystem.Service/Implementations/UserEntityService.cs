using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Service.Interfaces;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;
using TaskManagmentSystem.DAL.Repositories;
using TaskManagmentSystem.Domain.ViewModels;

namespace TaskManagmentSystem.Service.Implementations
{
    public class UserEntityService : IUserEntityService
    {
        private readonly IUserEntityRepository userEntityRepository;

        public UserEntityService(IUserEntityRepository userEntityRepository)
        {
            this.userEntityRepository = userEntityRepository;
        }

        public async Task<BaseResponse<IEnumerable<UserEntity>>> GetAllUsersEntity()
        {
            BaseResponse<IEnumerable<UserEntity>> baseResponse = new BaseResponse<IEnumerable<UserEntity>>();
            try
            {
                List<UserEntity> users = await userEntityRepository.GetAll();

                if (users.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserEntity>> 
                { 
                    Description = $"[GetAllUsersEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityByName(string name)
        {
            BaseResponse<IEnumerable<UserEntity>> baseResponse = new BaseResponse<IEnumerable<UserEntity>>();
            try
            {
                List<UserEntity> users = userEntityRepository.GetByUserName(name);

                if (users.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserEntity>>
                {
                    Description = $"[GetUsersEntityByName]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityByPosition(Position position)
        {
            BaseResponse<IEnumerable<UserEntity>> baseResponse = new BaseResponse<IEnumerable<UserEntity>>();
            try
            {
                List<UserEntity> users = userEntityRepository.GetByPosition(position);

                if (users.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserEntity>>
                {
                    Description = $"[GetUsersEntityByPosition]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }
    
        public async Task<BaseResponse<bool>> CreateUserEntity(UserEntityViewModel model)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await userEntityRepository.Create(new UserEntity() 
                { 
                    Name = model .Name,
                    Position = model.Position
                });
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[CreateUserEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<UserEntity>> GetUserEntity(int Id)
        {
            BaseResponse<UserEntity> baseResponse = new BaseResponse<UserEntity>();
            try 
            {
                var user = await userEntityRepository.GetById(Id);
                if (user != null)
                {                    
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = user;
                    return baseResponse;   
                }
                else
                {
                    baseResponse.Description = "Пользователя не существует в бд";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserEntity>
                {
                    Description = $"[GetUserEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        
        }

        public async Task<BaseResponse<bool>> DeleteUserEntity(int Id)
        {
            BaseResponse<bool> baseResponse = new BaseResponse<bool>();
            try
            {
                var user = await userEntityRepository.GetById(Id);
                if (user != null)
                {
                    await userEntityRepository.Delete(user);
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = true;
                }
                else
                {
                    baseResponse.Description = "Пользователя нет в бд";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Data = true;
                }

                return baseResponse;

            }catch(Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteUserEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException,
                    Data = false
                };
            }

        }

        public async Task<BaseResponse<UserEntity>> EditUserEntity(int id, UserEntityViewModel model)
        {
            var baseResponse = new BaseResponse<UserEntity>();
            try
            {
                var user = await userEntityRepository.GetById(id);
                if (user == null) 
                {
                    baseResponse.StatusCode= StatusCode.ObjectNotFound;
                    baseResponse.Description = "ObjectNotFound";
                    return baseResponse;
                }
                user.Position = model.Position; // надо решить потом
                user.Name = model.Name;

                await userEntityRepository.Update(user);

                baseResponse.StatusCode = StatusCode.OK;
                
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<UserEntity>
                {
                    Description = $"[EditUserEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<UserEntity>> GetUsersEntityLowPosition(int id)
   
        {
            BaseResponse<IEnumerable<UserEntity>> baseResponse = new BaseResponse<IEnumerable<UserEntity>>();
            try
            {
                
                                                
                List<UserEntity> users = userEntityRepository.GetUsersLowPositionSameDepartment(id);

                if (users.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserEntity>>
                {
                    Description = $"[GetUsersEntityLowPosition]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }
    }
}