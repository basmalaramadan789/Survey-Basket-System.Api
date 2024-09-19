using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Contracts.Authentication
{
    public record AuthResponse(
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresIn,
        //RefreshToken 
        string RefreshToken,
        DateTime RefreshTokenExpiration
        );
  
}
