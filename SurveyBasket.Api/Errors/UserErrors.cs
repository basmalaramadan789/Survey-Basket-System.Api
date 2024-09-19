using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors
{
    public static class UserErrors
    {
        public static readonly Error InvalidCredential = new("user invalid credentail", "invalid email/password",StatusCodes.Status401Unauthorized);



        public static readonly Error DuplicatedEmail = 
            new("user.Duplicated Email", "another user with the same email is already exist",StatusCodes.Status409Conflict);

        public static readonly Error EmailNotConfirmed =
            new("user.EmailNotConfirmed", "Email is Not Confirmed", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidCode =
           new("user.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);

        public static readonly Error DuplicatedEmailConfirmation =
           new("user.DuplicatedEmailConfirmation", "Email Already Confirmed", StatusCodes.Status400BadRequest);

        public static readonly Error DisabledUser =
          new("user.DisabledUser", "DisabledUser", StatusCodes.Status400BadRequest);


        public static readonly Error UserNotFound =
         new("user.UserNotFound", "UserNotFound", StatusCodes.Status404NotFound);


        public static readonly Error InvalidRoles =
                 new("user.InvalidRoles", "InvalidRoles", StatusCodes.Status400BadRequest);

    }
}
