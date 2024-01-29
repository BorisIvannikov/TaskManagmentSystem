using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;

namespace TaskManagmentSystem.DAL.Repositories
{
    public class TaskEntityRepository : ITaskEntityRepository
    {
        private readonly ApplicationDBContext _db;

        public TaskEntityRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(TaskEntity entity)
        {
            await _db.TaskEntity.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(TaskEntity entity)
        {
            _db.TaskEntity.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskEntity>> GetAll()
        {
            return await _db.TaskEntity.
                Include(x => x.Initiator).
                Include(x => x.Recipient).ToListAsync();
        }

        public async Task<TaskEntity?> GetById(int id)
        {
            return await _db.TaskEntity.
                Include(x=> x.Initiator).
                Include(x => x.Recipient).
                FirstOrDefaultAsync(x => x.TaskEntityId == id);
        }

        public List<TaskEntity> GetByUserInitiator(UserEntity user)
        {
            return _db.TaskEntity.Where(x => x.Initiator == user).
                Include(x => x.Initiator).
                Include(x => x.Recipient).ToList();
        }

        public List<TaskEntity> GetByUserInitiator(int userId)
        {
            return _db.TaskEntity.Where(x => x.InitiatorId == userId).
                Include(x => x.Initiator).
                Include(x => x.Recipient).ToList();
        }

        public List<TaskEntity> GetByUserRecipient(UserEntity user)
        {
            return _db.TaskEntity.Where(x => x.Recipient == user).
                Include(x => x.Initiator).
                Include(x => x.Recipient).ToList();
        }

        public List<TaskEntity> GetByUserRecipient(int userId)
        {
            return _db.TaskEntity.Where(x => x.RecipientId == userId).
                Include(x => x.Initiator).
                Include(x => x.Recipient).ToList();
        }

        public async Task<List<TaskEntity>> Select()
        {
            return await _db.TaskEntity.ToListAsync();
        }

        public async Task<TaskEntity> Update(TaskEntity entity)
        {
            _db.TaskEntity.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
