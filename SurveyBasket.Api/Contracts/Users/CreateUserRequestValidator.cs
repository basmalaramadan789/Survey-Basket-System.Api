﻿using SurveyBasket.Api.Abstractions.Const;

namespace SurveyBasket.Api.Contracts.Users
{
    public class CreateUserRequestValidator:AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty()
           .EmailAddress();

            RuleFor(x => x.Password)
            .NotEmpty();
            

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 100);

            RuleFor(x => x.Roles)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Roles)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("You cannot add duplicated role for the same user")
                .When(x => x.Roles != null);
        }
    }
}
