using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Abstractions.Consts;
using SurveyBasket.Api.Contracts.Users;
using SurveyBasket.Api.Errors;


namespace SurveyBasket.Api.Services
{
    public class UserService(UserManager<ApplicationUser> userManager,IRoleService roleService,ApplicationDbContext context ): IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IRoleService _roleService = roleService;
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<UserProfileResponse>> getProfileAsync(string userId)
        {
            var user = await _userManager.Users
                .Where(x=>x.Id == userId)
                .ProjectToType<UserProfileResponse>()
                .SingleAsync();

            return Result.Succuss(user);
        } 

        public async Task<Result> UpdateProfileAsync(string userId ,UpdateProfileRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user = request.Adapt(user);

            await _userManager.UpdateAsync(user!);

            return Result.Success(); 
        }

        public async Task<Result> ChangePasswordAsync(string userId ,ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result =await _userManager.ChangePasswordAsync(user!, request.CurrentPassword,request.NewPassword);

            if(result.Succeeded )
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

            
        }


        public async Task<Result<UserResponse>> GetAsync(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);

            var userRoles = await _userManager.GetRolesAsync(user);

            var response = (user, userRoles).Adapt<UserResponse>();

            return Result.Succuss(response); 

        }




        //retrieve all users with their roles
        public async Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await(from u in _context.Users
              join ur in _context.UserRoles
              on u.Id equals ur.UserId
              join r in _context.Roles
              on ur.RoleId equals r.Id into roles
              where !roles.Any(x => x.Name == DefaultRoles.Member)
              select new
              {
                  u.Id,
                  u.FirstName,
                  u.LastName,
                  u.Email,
                  u.IsDesabled,
                  Roles = roles.Select(x => x.Name!).ToList()
              }
                )
                .GroupBy(u => new { u.Id, u.FirstName, u.LastName, u.Email, u.IsDesabled })
                .Select(u => new UserResponse
                (
                    u.Key.Id,
                    u.Key.FirstName,
                    u.Key.LastName,
                    u.Key.Email,
                    u.Key.IsDesabled,
                    u.SelectMany(x => x.Roles)
                ))
               .ToListAsync(cancellationToken);

        //create new user
        public async Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

            var allowedRoles = await _roleService.GetAllAsync();

            if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
                return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

            var user = request.Adapt<ApplicationUser>();

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, request.Roles);

                var response = (user, request.Roles).Adapt<UserResponse>();

                return Result.Succuss(response);
            }

            var error = result.Errors.First();

            return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }


        public async Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id, cancellationToken);

            if (emailIsExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var allowedRoles = await _roleService.GetAllAsync();

            if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
                return Result.Failure(UserErrors.InvalidRoles);

            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            user = request.Adapt(user);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _context.UserRoles
                    .Where(x => x.UserId == id)
                    .ExecuteDeleteAsync(cancellationToken);

                await _userManager.AddToRolesAsync(user, request.Roles);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }


        public async Task<Result> ToggleStatus(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            user.IsDesabled = !user.IsDesabled;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> Unlock(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }




    }


}
