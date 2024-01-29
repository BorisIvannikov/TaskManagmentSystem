using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.DAL.Interfaces
{
    public interface IUserEntityRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetById(int id);
        List<UserEntity> GetByUserName(string userName);
        List<UserEntity> GetByPosition(Position position);
        Task<List<UserEntity>> GetAll();

        List<UserEntity> GetUsersLowPositionSameDepartment(int id);


    }
}
