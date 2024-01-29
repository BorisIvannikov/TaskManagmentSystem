using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create (T entity);
        Task<List<T>> Select();
        Task<bool> Delete(T entity);
        Task<T> Update (T entity);
    }
}
