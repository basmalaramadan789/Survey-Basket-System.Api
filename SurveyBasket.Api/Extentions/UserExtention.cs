using System.Security.Claims;

namespace SurveyBasket.Api.Extentions
{
    //class for get User ID
    public static class UserExtention
    {
        //extention method
        public  static string? GetUserId(this ClaimsPrincipal user )
        {
           return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
