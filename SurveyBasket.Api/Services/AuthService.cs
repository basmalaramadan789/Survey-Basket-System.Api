using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Abstractions.Consts;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Errors;
using SurveyBasket.Api.Helpers;
using System.Security.Cryptography;
using System.Text;


namespace SurveyBasket.Api.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtProvider jwtProvider,
        ILogger<AuthService> logger,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context
        ) : IAuthService
       
    {
        private readonly UserManager<ApplicationUser> _userManager=userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ApplicationDbContext _context = context;
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            //check user?
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure<AuthResponse>(new Error("user invalid credentail","invalid email/password",StatusCodes.Status400BadRequest));
            }
            if (user.IsDesabled)
                return Result.Failure<AuthResponse>(UserErrors.DisabledUser);
            

            var result = await _signInManager.PasswordSignInAsync(user, password,false, true);
            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var userPermissions = await _context.Roles
                    .Join(_context.RoleClaims,role=>role.Id,claim=>claim.RoleId,
                    (role,claim)=> new {role , claim})
                    .Where(x => userRoles.Contains(x.role.Name!))
                    .Select(x => x.claim.ClaimValue!)
                    .Distinct()
                    .ToListAsync(cancellationToken: cancellationToken);

                //generate JwT Token
                var (token, expirein) = _jwtProvider.GenerateToken(user,userRoles, userPermissions);
                //RefreshToken
                var refreshToken = GenerateRefereshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                //save refreshToken in db
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration

                });
                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expirein, refreshToken, refreshTokenExpiration);

                return Result.Succuss(response);

            }
            return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredential);
            
            
        }


        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId= _jwtProvider.ValidateToken(token);
            if (userId is null)
            {
                return null;
            }
            var user =await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x=>x.Token==refreshToken && x.IsActive);

            if (userRefreshToken == null) 
                return null;
            userRefreshToken.RevocedOn = DateTime.UtcNow;

            var userRoles = await _userManager.GetRolesAsync(user);

            var userPermissions = await _context.Roles
                .Join(_context.RoleClaims, role => role.Id, claim => claim.RoleId,
                (role, claim) => new { role, claim })
                .Where(x => userRoles.Contains(x.role.Name!))
                .Select(x => x.claim.ClaimValue!)
                .Distinct()
                .ToListAsync(cancellationToken: cancellationToken);

            //New refreshToken
            var (newToken, expirein) = _jwtProvider.GenerateToken(user,userRoles, userPermissions);
            //RefreshToken
            var newRefreshToken = GenerateRefereshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            //save refreshToken in db
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration

            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expirein, newRefreshToken, refreshTokenExpiration);

        }

        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken == null)
                return false;

            userRefreshToken.RevocedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
        }


        //Registraion
        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            // ensure unique email
            var emailIsExit = await _userManager.Users.AnyAsync(x=>x.Email == request.Email , cancellationToken);
            if(emailIsExit)
            {
                return Result.Failure(UserErrors.DuplicatedEmail);
            }

            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user,request.Password);
            if (result.Succeeded)
            {
                //generate code
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confiramation Code: {code}",code);

                var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
                //ToDo:Send Email
                var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                    new Dictionary<string, string>
                    {
                        {"{{name}}",user.FirstName},
                        {"{{action_url}}",$"{origin}/authentication/EmailConfirmation?userId={user.Id}&code={code}" }
                    }
                    );
                await _emailSender.SendEmailAsync(user.Email!, "survey basket : Email Confirmation", emailBody);
                return Result.Success();
            }

            var error = result.Errors.First();
            return Result.Failure(new Error(
                error.Code,
                error.Description,
                StatusCodes.Status400BadRequest
                ));


        }


        public async Task<Result> ConfirmEmailAsync(ConfirmationEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedEmailConfirmation);

            var code = request.Code;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidCode);
            }

            //confirm email
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, DefaultRoles.Member);
                return Result.Success();
            }

            var error = result.Errors.First();
            return Result.Failure(new Error(
                error.Code,
                error.Description,
                StatusCodes.Status400BadRequest
            ));


        }


        public async Task<Result> ResendConfirmEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                  return Result.Success();
            

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedEmailConfirmation);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confiramation Code: {code}", code);


            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            // Send Email
            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                new Dictionary<string, string>
                {
                        {"{{name}}",user.FirstName},
                        {"{{action_url}}",$"{origin}/authentication/EmailConfirmation?userId={user.Id}&code={code}" }
                }
                );

            await _emailSender.SendEmailAsync(user.Email!, "survey basket : Email Confirmation", emailBody);


            return Result.Success();
        }


        //Forget password
        public async Task<Result> SendResetPasswordCodeAsync(string Email)
        {
            if (await _userManager.FindByEmailAsync(Email) is not { } user)
                return Result.Success();
            if (!user.EmailConfirmed)
            {
                return Result.Failure(UserErrors.EmailNotConfirmed);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confiramation Code: {code}", code);

           await SendResetPasswordEmail(user, code);

            return Result.Success();

        }

        //Reset Password
        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidCode);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _userManager.ResetPasswordAsync(user,code,request.NewPassword); 
            }
            catch ( FormatException )
            {
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
            }

            if (result.Succeeded)
            {
                return Result.Success();
            }

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));



        }

        private static string GenerateRefereshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private async Task SendResetPasswordEmail(ApplicationUser user , string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            
            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                new Dictionary<string, string>
                {
                        {"{{name}}",user.FirstName},
                        {"{{action_url}}",$"{origin}/authentication/forgetPassword?email={user.Email}&code={code}" }
                }
                );
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "survey basket : Change Password", emailBody));

            await Task.CompletedTask;
        }

        
    }
}
