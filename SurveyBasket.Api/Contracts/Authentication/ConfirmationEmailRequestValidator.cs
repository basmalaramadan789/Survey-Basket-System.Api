namespace SurveyBasket.Api.Contracts.Authentication
{
    public class ConfirmationEmailRequestValidato : AbstractValidator<ConfirmationEmailRequest>
    {
        public ConfirmationEmailRequestValidato()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Code)
                .NotEmpty();



        }



    }
}

