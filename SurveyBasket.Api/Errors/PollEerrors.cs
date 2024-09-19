using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors
{
    public static class PollEerrors
    {
        public static readonly Error pollNotFound =
            new("poll not found", "no poll was found with the id",StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedPollTitle  =
            new("poll title found", "another poll with the same title is already exist", StatusCodes.Status409Conflict);
    }
}
