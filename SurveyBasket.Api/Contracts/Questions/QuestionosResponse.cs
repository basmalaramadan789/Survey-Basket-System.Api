using SurveyBasket.Api.Contracts.Answers;

namespace SurveyBasket.Api.Contracts.Questions
{
    public record QuestionosResponse(
        int Id,
        string Content ,
        IEnumerable<AnswerResponse> Answers


    );
    
}
