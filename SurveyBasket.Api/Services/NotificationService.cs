
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SurveyBasket.Api.Helpers;

namespace SurveyBasket.Api.Services
{
    public class NotificationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor,IEmailSender emailSender) : INotificationService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IEmailSender _emailSender = emailSender;

        public async Task SendNewPollNotification(int? pollid = null)
        {
            IEnumerable<Poll> polls = [];

            if (pollid.HasValue)
            {
                var poll =await _context.polls.SingleOrDefaultAsync(x=>x.Id == pollid && x.IsPublished);

                polls = [poll!];
            }
            else
            {
                polls = await _context.polls.
                    Where(x=>x.IsPublished && x.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))
                    .AsNoTracking()
                    .ToListAsync();
            }
            //TODO select members only
            var users = await _context.Users.ToListAsync();
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            foreach(var poll in polls)
            {
                foreach(var user in users)
                {
                    var placeHolders = new Dictionary<string, string>
                    {
                        {"{{name}}",user.FirstName },
                        {"{{pollTill}}",poll.Title },
                        {"{{endDate}}",poll.EndsAt.ToString() },
                        {"{{url}}",$"{origin}/polls/start/{poll.Id}" }
                    };
                    var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeHolders);
                    await _emailSender.SendEmailAsync(user.Email!,"surevy basket",body);
                }
            }
        }
    }
}
