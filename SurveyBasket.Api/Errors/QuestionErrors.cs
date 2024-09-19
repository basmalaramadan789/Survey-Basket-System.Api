using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors
{
    public static class QuestionErrors
    {
        public static readonly Error questionNotFound =
            new("Question not found", "no Question was found with the id", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedQustion  =
            new("Question.Duplicated", "another Question with the same content is already exist", StatusCodes.Status409Conflict);
    }
}
