using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.Contracts.Questions;

namespace SurveyBasket.Api.Services
{
    public interface IQuestionService
    {
        Task<Result<PaginatedList<QuestionosResponse>>> GetAllAsync(int pollId, RequestFilter filters, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<QuestionosResponse>>> GetAvailabeAsync(int pollId,string userId ,CancellationToken cancellationToken = default);
        Task<Result<QuestionosResponse>> GetAsync(int pollId,int id ,CancellationToken cancellationToken = default);
        Task<Result<QuestionosResponse>> AddAsync(int pollId , QuestionsRequest request, CancellationToken cancellationToken =default);
        Task<Result> UpdateAsync(int pollId, int id, QuestionsRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(int pollId,int id, CancellationToken cancellationToken = default);
    }
}
