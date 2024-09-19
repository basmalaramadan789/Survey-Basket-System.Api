namespace SurveyBasket.Api.Authentication
{
    public interface IJwtProvider
    {
        (string token , int ExpiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> rols, IEnumerable<string> permissions);
        //refresh token
        string? ValidateToken(string token);
    }
}
