namespace SurveyBasket.Api.Contracts.Users
{
    public class UpdateProfileRequstValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequstValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3,100);

            RuleFor(x => x.Lastname)
               .NotEmpty()
               .Length(3, 100);



        }

    }
}