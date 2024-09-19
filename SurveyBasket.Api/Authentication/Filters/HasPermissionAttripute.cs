using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Api.Authentication.Filters
{
    public class HasPermissionAttripute(string permission) : AuthorizeAttribute(permission)
    {
    }
}
