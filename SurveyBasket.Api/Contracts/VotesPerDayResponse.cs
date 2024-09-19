namespace SurveyBasket.Api.Contracts
{
    public record VotesPerDayResponse(
        
      DateOnly Date,
      int NumberOfVotes
      
);
    
    
}
