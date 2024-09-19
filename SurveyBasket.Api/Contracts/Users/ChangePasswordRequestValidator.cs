namespace SurveyBasket.Api.Contracts.Users
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();


            RuleFor(x => x.NewPassword)
               .NotEmpty()
               .NotEqual(x => x.CurrentPassword)
               .WithMessage("new password musnt match the current password");



        }
    }
}