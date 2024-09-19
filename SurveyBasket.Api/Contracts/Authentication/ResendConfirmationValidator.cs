namespace SurveyBasket.Api.Contracts.Authentication
{
    public class ResendConfirmationValidator : AbstractValidator<ResendConfirmationEmailRequest>
    {
        public ResendConfirmationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

        }

    }
}
