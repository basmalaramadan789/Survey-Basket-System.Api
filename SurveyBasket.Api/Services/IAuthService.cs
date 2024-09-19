using SurveyBasket.Api.Abstraction;

namespace SurveyBasket.Api.Services
{
    public interface IAuthService
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email ,string password,CancellationToken cancellationToken=default);
        Task<AuthResponse?> GetRefreshTokenAsync(string token,string refreshToken,CancellationToken cancellationToken=default);
        Task<bool> RevokeRefreshTokenAsync(string token,string refreshToken,CancellationToken cancellationToken=default);

        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(ConfirmationEmailRequest request);
        Task<Result> ResendConfirmEmailAsync(ResendConfirmationEmailRequest request);


        Task<Result> SendResetPasswordCodeAsync(string Email);
        Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
