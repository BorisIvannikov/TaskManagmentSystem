using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.AdditionalsFunction;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Service.Implementations
{
    public class UserEntityProfileService : IUserEntityProfileService
    {
        private readonly IUserEntityProfileRepository userEntityProfileRepository;
        private readonly IUserEntityRepository userEntityRepository;

        public UserEntityProfileService(IUserEntityProfileRepository userEntityProfileRepository, IUserEntityRepository userEntityRepository)
        {
            this.userEntityProfileRepository = userEntityProfileRepository;
            this.userEntityRepository = userEntityRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> CreateUserEntityProfile(UserEntityProfileViewModel model)
        {
            try
            {
                
                var userExist = (await userEntityProfileRepository.Select()).FirstOrDefault(u => u.Login == model.Login);
                if(userExist != null) 
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Данный логин уже занят",
                        StatusCode = Domain.Enums.StatusCode.DataAlreadyExist
                    };
                }

                UserEntity userEntity = new UserEntity() { Name = model.Name, Position = model.Position };

                UserEntityProfile profile = new() {
                    Name = model.Name,
                    Position = model.Position,
                    Login = model.Login,
                    Password = Domain.AdditionalsFunction.AdditionalFunction.HashPassword(model.Password),
                    UserEntity = userEntity,

                };

                await userEntityProfileRepository.Create(profile);

                var result = Authenticate(profile);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "пользователь зареган",
                    StatusCode = Domain.Enums.StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[CreateUserEntityProfile]" + ex.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalException
                };
            }
        }

        private ClaimsIdentity Authenticate(UserEntityProfile profile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, profile.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, profile.Position.ToString()),
                new Claim("UserEntityId", profile.UserEntityProfileId.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var userExist = (await userEntityProfileRepository.Select()).FirstOrDefault(u => u.Login == model.Login);
                if (userExist == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Логин не найден",
                        StatusCode = Domain.Enums.StatusCode.ObjectNotFound
                    };
                }

                if(AdditionalFunction.HashPassword(model.Password) != userExist.Password)
                {
                    return new BaseResponse<ClaimsIdentity>() 
                    {
                        Description = "Пароль не верен"
                    };
                }

                var result = Authenticate(userExist);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = Domain.Enums.StatusCode.OK,
                    Description = "Выполняется вход"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Login]" + ex.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalException
                };
            }
        }


        public Task<BaseResponse<UserEntityProfileViewModel>> GetUserEntityProfile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> UpdateUserEntityProfile(int id, UserEntityProfileViewModel model)
        {
            throw new NotImplementedException();
        }

    }
}
