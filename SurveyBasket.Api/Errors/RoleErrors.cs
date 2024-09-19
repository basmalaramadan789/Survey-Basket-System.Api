using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors
{
    public static class RoleErrors
    {
        public static readonly Error RoleNotFound = 
            new("Role.RoleNotFound", "role not found ",StatusCodes.Status404NotFound);



        public static readonly Error DuplicatedRole =
            new("Role.Duplicated", "another Role with this Name Found ", StatusCodes.Status409Conflict);


        public static readonly Error InvalidPermissions =
            new("Role.invalid", "invalid permissions", StatusCodes.Status400BadRequest);


    }
}
