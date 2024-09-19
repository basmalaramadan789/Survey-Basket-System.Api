using SurveyBasket.Api.Errors;

namespace SurveyBasket.Api.Services
{
    public class PollService(ApplicationDbContext context) : IPollService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.polls.AsNoTracking().ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<PollResponse>> GetCurrentAsyncV1(CancellationToken cancellationToken = default)
        {// return the polls that is publish and its time between start date and end date
            return await _context.polls 
                .Where(x=>x.IsPublished && x.StartsAt <=DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ProjectToType<PollResponse>()
                .ToListAsync(cancellationToken);

        }

        public async Task<IEnumerable<PollResponseV2>> GetCurrentAsyncV2(CancellationToken cancellationToken = default)
        {
            return await _context.polls
                .Where(x => x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ProjectToType<PollResponseV2>()
                .ToListAsync(cancellationToken);
        }


            public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll= await _context.polls.FindAsync(id, cancellationToken);
            return poll is not null ? Result.Succuss(poll.Adapt<PollResponse>()): 
                Result.Failure<PollResponse>(PollEerrors.pollNotFound);
        }

        public async Task<Result<PollResponse>> AddAsync(CreatePollRequest request, CancellationToken cancellationToken = default)
        {
            // handle exeption of duplicated title
            var isExistingTitle = await _context.polls.AnyAsync(x => x.Title == request.Title, cancellationToken: cancellationToken);
            if (isExistingTitle)
            {
              return  Result.Failure<PollResponse>(PollEerrors.DuplicatedPollTitle);
            }
               
            var poll = request.Adapt<Poll>();
            await _context.polls.AddAsync(poll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Succuss(poll.Adapt<PollResponse>());
        }

        public async Task<Result> UpdateAsync(int id, CreatePollRequest poll, CancellationToken cancellationToken = default)
        {
            var currentpoll = await _context.polls.FindAsync(id, cancellationToken);
            if (currentpoll is null)
            {
                return Result.Failure(PollEerrors.pollNotFound);
            }
            currentpoll.Title = poll.Title;
            currentpoll.Summary = poll.Summary;
            currentpoll.StartsAt = poll.StartsAt;
            currentpoll.EndsAt = poll.EndsAt;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.polls.FindAsync(id, cancellationToken);
            if (poll is null)
            {
                return Result.Failure(PollEerrors.pollNotFound);
            }
             _context.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);

             return Result.Success();

        }
        public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var currentpoll = await _context.polls.FindAsync(id, cancellationToken);
            if (currentpoll is null)
            {
                return Result.Failure(PollEerrors.pollNotFound);
            }

            currentpoll.IsPublished = !currentpoll.IsPublished;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }

}
