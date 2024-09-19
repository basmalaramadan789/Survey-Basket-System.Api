using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors
{
    public static class VoteEerror
    {
        public static readonly Error InvalidQuestions =
            new("Vote.InvalidQuestions", "InvalidQuestions",StatusCodes.Status400BadRequest);

        public static readonly Error DuplicatedVote  =
            new("vote . DuplicatedVote", "this user already voted before", StatusCodes.Status409Conflict);
    }
}
