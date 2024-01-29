using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagmentSystem.DAL.Interfaces;
using TaskManagmentSystem.Domain.Entity;
using TaskManagmentSystem.Domain.Enums;
using TaskManagmentSystem.Domain.Response;
using TaskManagmentSystem.Service.Interfaces;

namespace TaskManagmentSystem.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        public async Task<BaseResponse<bool>> CreateFeedback(Feedback feedback)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                await feedbackRepository.Create(new Feedback {
                    Id = feedback.Id,
                    BornTime = DateTime.Now,
                    Name = feedback.Name,
                    Message = feedback.Message
                });

                result.StatusCode = StatusCode.OK;
                result.Data = true;

                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Description = $"[CreateFeedback]" + ex.Message,
                    StatusCode = StatusCode.InternalException,
                    Data = true
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Feedback>>> GetAllFeedbacks()
        {
            BaseResponse<IEnumerable<Feedback>> result = new BaseResponse<IEnumerable<Feedback>>(); 
            try
            {
                List<Feedback> feedbacks = await feedbackRepository.Select();

                result.StatusCode = Domain.Enums.StatusCode.OK;
                result.Data = feedbacks;

                return result;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Feedback>>
                {
                    Description = $"[GetAllFeedbacks]" + ex.Message,
                    StatusCode = StatusCode.InternalException
                };
            }
        }
    }
}
