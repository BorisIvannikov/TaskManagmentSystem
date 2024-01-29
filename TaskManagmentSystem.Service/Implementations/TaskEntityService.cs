using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.DAL.Repositories;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Service.Implementations
{
    public class TaskEntityService : ITaskEntityService
    {
        private readonly ITaskEntityRepository taskEntityRepository;

        public TaskEntityService(ITaskEntityRepository taskEntityRepository)
        {
            this.taskEntityRepository = taskEntityRepository;
        }

        public async Task<BaseResponse<bool>> CreateTaskEntity(TaskEntityViewModel model, int initiatorId)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                await taskEntityRepository.Create(new TaskEntity()
                {
                    Subject = model.Subject,
                    Description = model.Description,
                    Priority = model.Priority,
                    InitiatorId = initiatorId,
                    RecipientId = model.RecipientID,
                    BornDateTime = DateTime.Now,
                    IsDone = false
                });
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[CreateTaskEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteTaskEntity(int id)
        {
            BaseResponse<bool> baseResponse = new();
            try
            {
                var task = await taskEntityRepository.GetById(id);
                if (task != null)
                {
                    await taskEntityRepository.Delete(task);
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = true;
                }
                else
                {
                    baseResponse.Description = "задачи нет в бд";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Data = true;
                }

                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[DeleteTaskEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException,
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<TaskEntity>> EditTaskEntity(int id, TaskEntityViewModel model)
        {
            var baseResponse = new BaseResponse<TaskEntity>();
            try
            {
                var task = await taskEntityRepository.GetById(id);
                if (task == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "ObjectNotFound";
                    return baseResponse;
                }
                task.Subject = model.Subject;
                task.Description = model.Description;
                task.Priority = model.Priority;

                await taskEntityRepository.Update(task);

                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<TaskEntity>
                {
                    Description = $"[EditTaskEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<TaskEntity>>> GetAllTasksEntity()
        {

            BaseResponse<IEnumerable<TaskEntity>> baseResponse = new ();
            try
            {
                List<TaskEntity> tasks = await taskEntityRepository.GetAll();

                if (tasks.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = tasks;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>
                {
                    Description = $"[GetAllTasksEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public async Task<BaseResponse<TaskEntity>> GetTaskEntity(int id)
        {
            BaseResponse<TaskEntity> baseResponse = new BaseResponse<TaskEntity>();
            try
            {
                var task = await taskEntityRepository.GetById(id);
                if (task != null)
                {
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Data = task;
                    return baseResponse;
                }
                else
                {
                    baseResponse.Description = "Задачи не существует в бд";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<TaskEntity>
                {
                    Description = $"[GetTaskEntity]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByInitiator(UserEntity user)
        {
            BaseResponse<IEnumerable<TaskEntity>> baseResponse = new ();
            try
            {
                List<TaskEntity> tasks = taskEntityRepository.GetByUserInitiator(user);

                if (tasks.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = tasks;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>
                {
                    Description = $"[GetTasksEntityByInitiator]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByInitiator(int userId)
        {
            BaseResponse<IEnumerable<TaskEntity>> baseResponse = new();
            try
            {
                List<TaskEntity> tasks = taskEntityRepository.GetByUserInitiator(userId);

                if (tasks.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = tasks;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>
                {
                    Description = $"[GetTasksEntityByInitiator]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByRecipient(UserEntity user)
        {
            BaseResponse<IEnumerable<TaskEntity>> baseResponse = new();
            try
            {
                List<TaskEntity> tasks = taskEntityRepository.GetByUserRecipient(user);

                if (tasks.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = tasks;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>
                {
                    Description = $"[GetTasksEntityByRecipient]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByRecipient(int userId)
        {
            BaseResponse<IEnumerable<TaskEntity>> baseResponse = new();
            try
            {
                List<TaskEntity> tasks = taskEntityRepository.GetByUserRecipient(userId);

                if (tasks.Count == 0)
                {
                    baseResponse.Description = "В БД Ноль элементов";
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    return baseResponse;
                }

                baseResponse.Data = tasks;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<TaskEntity>>
                {
                    Description = $"[GetTasksEntityByRecipient]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }

        public async Task<BaseResponse<bool>> PerformATask(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var task = await taskEntityRepository.GetById(id);
                if (task == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjectNotFound;
                    baseResponse.Description = "ObjectNotFound";
                    return baseResponse;
                }

                task.IsDone = true;

                await taskEntityRepository.Update(task);

                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Description = $"[PerformATask]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }
    }
}
