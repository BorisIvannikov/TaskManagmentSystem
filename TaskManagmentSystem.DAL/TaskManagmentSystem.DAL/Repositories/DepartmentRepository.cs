using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;

namespace TaskManagmentSystem.DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDBContext _db;

        public DepartmentRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Department entity)
        {
            await _db.Department.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Department entity)
        {
            _db.Department.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Department>> Select()
        {
            return await _db.Department.ToListAsync();
        }

        public async Task<Department> Update(Department entity)
        {
            _db.Department.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
