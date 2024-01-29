using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Domain.ViewModels;

namespace TaskManagmentSystem.Service.Interfaces
{
    public interface ITaskEntityService
    {
        public Task<BaseResponse<IEnumerable<TaskEntity>>> GetAllTasksEntity();
        public Task<BaseResponse<bool>> CreateTaskEntity(TaskEntityViewModel model, int initiatorId);
        public Task<BaseResponse<bool>> DeleteTaskEntity(int  id);
        public Task<BaseResponse<TaskEntity>> GetTaskEntity(int id);
        public Task<BaseResponse<TaskEntity>> EditTaskEntity(int id, TaskEntityViewModel model); 
        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByInitiator(UserEntity user);
        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByInitiator(int userId);
        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByRecipient(UserEntity user);
        public BaseResponse<IEnumerable<TaskEntity>> GetTasksEntityByRecipient(int userId);
        public Task<BaseResponse<bool>> PerformATask(int id);

    }
}
