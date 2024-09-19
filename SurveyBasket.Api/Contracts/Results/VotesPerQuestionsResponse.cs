namespace SurveyBasket.Api.Contracts.Results
{
    public record VotesPerQuestionsResponse(
        string Qeustion,
        IEnumerable<VotesPerAnswerResponse> SelectedAnswers
    );
    
}
