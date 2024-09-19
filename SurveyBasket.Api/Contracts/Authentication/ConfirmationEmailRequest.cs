namespace SurveyBasket.Api.Contracts.Authentication
{
    public record ConfirmationEmailRequest(
      string UserId,
      string Code
);
    
}
