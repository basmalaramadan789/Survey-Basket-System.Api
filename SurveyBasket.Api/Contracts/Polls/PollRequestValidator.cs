using System.Runtime.CompilerServices;

namespace SurveyBasket.Api.Contracts.Polls
{
    public class PollRequestValidator : AbstractValidator<CreatePollRequest>
    {
        public PollRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Summary)
                .NotEmpty().
                Length(3, 1500);

            RuleFor(x => x.StartsAt)
                .NotEmpty().
                GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.EndsAt)
                .NotEmpty();

            RuleFor(x => x)
                .Must(HasValidateDate)
                .WithName(nameof(CreatePollRequest.EndsAt))
                .WithMessage("{PropertyName} must greater than or equal to start date");


        }
        //custom validation
        private bool HasValidateDate(CreatePollRequest pollRequest)
        {
            return pollRequest.EndsAt >= pollRequest.StartsAt;
        }


    }
}
