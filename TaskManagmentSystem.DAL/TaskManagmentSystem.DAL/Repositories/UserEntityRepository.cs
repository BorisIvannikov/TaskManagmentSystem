using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.DAL.Repositories
{
    public class UserEntityRepository : IUserEntityRepository
    {
        private readonly ApplicationDBContext _db;

        public UserEntityRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<UserEntity>> Select()
        {
            return await _db.UserEntity.ToListAsync();
        }
        public async Task<bool> Create(UserEntity entity)
        {
            await _db.UserEntity.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
            
        }

        public async Task<bool> Delete(UserEntity entity)
        {
            _db.UserEntity.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<UserEntity>> GetAll()
        {
            return await _db.UserEntity.ToListAsync();
        }

        public async Task<UserEntity?> GetById(int id)
        {
            return await _db.UserEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);            
        }

        public List<UserEntity> GetByPosition(Position position)
        {            
            return _db.UserEntity.Where(x => x.Position == position).ToList();
        }

        public  List<UserEntity> GetByUserName(string Name)
        {
            return _db.UserEntity.Where(x => x.Name == Name).ToList();
        }

        public async Task<UserEntity> Update(UserEntity entity)
        {
            _db.UserEntity.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public List<UserEntity> GetUsersLowPositionSameDepartment(int id)
        {
            UserEntity user = _db.UserEntity
                .Include(x => x.Department)
                .FirstOrDefault(u => u.UserEntityId == id);

            var res =  _db.UserEntity
                .Where(u => ((int)u.Position >= (int)user.Position || u.Position == Position.Admin)     //Должность ниже или админ
                && (u.Department == user.Department || u.Department == null)                            //из того же отдела
                &&(u.UserEntityId != user.UserEntityId))                                                //проверка на то что это не ты же сам
                .ToList();

            return res;
        }

        /*public List<UserEntity> GetBySvoistvo(object svoistvo)
        {
            return _db.UserEntity.Where(x => x.GetType() == svoistvo.GetType()).ToList();
        }*/

    }
}
