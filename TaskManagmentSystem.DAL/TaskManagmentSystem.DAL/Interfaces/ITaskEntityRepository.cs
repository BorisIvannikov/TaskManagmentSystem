using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;

namespace TaskManagmentSystem.DAL.Interfaces
{
    public interface ITaskEntityRepository : IBaseRepository<TaskEntity>
    {
        Task<TaskEntity?> GetById(int id);
        Task<List<TaskEntity>> GetAll();
        List<TaskEntity> GetByUserInitiator(UserEntity user);
        List<TaskEntity> GetByUserInitiator(int userId);
        List<TaskEntity> GetByUserRecipient(UserEntity user);
        List<TaskEntity> GetByUserRecipient(int userId);
    }
}
