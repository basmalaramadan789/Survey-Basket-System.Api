namespace SurveyBasket.Api.Contracts.Polls
{
    public record CreatePollRequest(
        int Id,
        string Title,
        string Summary,
        DateOnly StartsAt,
        DateOnly EndsAt

    );
}
