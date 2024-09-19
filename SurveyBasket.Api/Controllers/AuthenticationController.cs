using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("")]
        public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
            //return authResult is null ? BadRequest("invalid email or password") : Ok(authResult);
            return authResult.IsSuccess ? Ok(authResult.Value) :/*BadRequest( authResult.Error)*/ authResult.ToProblem(); 

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return authResult is null ? BadRequest("invalid Token") : Ok(authResult);

        }

        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var IsRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
            return IsRevoked  ? Ok() :BadRequest("invalid Token") ;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]  RegisterRequest request, CancellationToken cancellationToken)
        {
            var authResult = await _authService.RegisterAsync(request , cancellationToken);
            //return authResult is null ? BadRequest("invalid Token") : Ok(authResult);
            return authResult.IsSuccess? Ok(): authResult.ToProblem();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmationEmailRequest request)
        {
            var authResult = await _authService.ConfirmEmailAsync(request);
            //return authResult is null ? BadRequest("invalid Token") : Ok(authResult);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

        [HttpPost("resend-confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendConfirmationEmailRequest request)
        {
            var authResult = await _authService.ResendConfirmEmailAsync(request);
            //return authResult is null ? BadRequest("invalid Token") : Ok(authResult);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }


        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var authResult = await _authService.SendResetPasswordCodeAsync(request.Email);
            //return authResult is null ? BadRequest("invalid Token") : Ok(authResult);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var authResult = await _authService.ResetPasswordAsync(request);
            //return authResult is null ? BadRequest("invalid Token") : Ok(authResult);
            return authResult.IsSuccess ? Ok() : authResult.ToProblem();
        }

    }
}
