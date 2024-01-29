using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Response;

namespace TaskManagmentSystem.Service.Interfaces
{
    public interface IFeedbackService
    {
        Task<BaseResponse<IEnumerable<Feedback>>> GetAllFeedbacks();

        Task<BaseResponse<bool>> CreateFeedback(Feedback feedback);
    }
}
