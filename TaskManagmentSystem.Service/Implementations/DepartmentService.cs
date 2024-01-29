using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<BaseResponse<IEnumerable<Department>>> GetAll()
        {
            var response = new BaseResponse<IEnumerable<Department>>();

            try
            {
                List<Department> departments = await departmentRepository.Select();

                if (departments.Count == 0)
                {
                    response.StatusCode = Domain.Enums.StatusCode.ObjectNotFound;
                    response.Description = "в бд нет таких объектов";
                    return response;
                }

                response.Data = departments;
                response.StatusCode = Domain.Enums.StatusCode.OK;

            }
            catch (Exception ex)
            {
                response.Description = @"[GetAll]" + ex.Message;
                response.StatusCode = Domain.Enums.StatusCode.InternalException;
            }

            return response;
        }


    }
}
