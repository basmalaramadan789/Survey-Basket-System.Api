using SurveyBasket.Api.Contracts;
using SurveyBasket.Api.Contracts.Results;
using SurveyBasket.Api.Errors;

namespace SurveyBasket.Api.Services
{
    public class ResultService(ApplicationDbContext context):IResultService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId ,CancellationToken cancellationToken =default)
        {
            var pollVotes = await _context.polls
                .Where(x=>x.Id == pollId)
                .Select(x => new PollVotesResponse(
                    x.Title,
                    x.Votes.Select(v => new VoteResponse(
                        $"{v.User.FirstName} {v.User.LastName}",
                        v.SubmittedOn,
                        v.VoteAnswers.Select(a=> new QuestionAnswerRespone(
                            a.Question.Content,
                            a.Answer.Content
                            ))
                        ))
                )).SingleOrDefaultAsync(cancellationToken);

            return pollVotes is null ? Result.Failure<PollVotesResponse>(PollEerrors.pollNotFound):
                Result.Succuss(pollVotes);
        }

       
        public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.polls.AnyAsync(x => x.Id == pollId, cancellationToken: cancellationToken);
            if (!pollIsExist)
            {
                return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollEerrors.pollNotFound);
            }
            var VotesPerDay =await _context.Votes
                .Where(x=>x.PollId == pollId)
                .GroupBy(x=> new {Date = DateOnly.FromDateTime(x.SubmittedOn)})
                .Select(g=> new VotesPerDayResponse (
                    g.Key.Date,
                    g.Count()

                )).ToListAsync(cancellationToken);  

            return Result.Succuss<IEnumerable<VotesPerDayResponse>>(VotesPerDay);


        }




        public async Task<Result<IEnumerable<VotesPerQuestionsResponse>>> GetVotesPerQuestionsAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.polls.AnyAsync(x => x.Id == pollId, cancellationToken: cancellationToken);
            if (!pollIsExist)
            {
                return Result.Failure<IEnumerable<VotesPerQuestionsResponse>>(PollEerrors.pollNotFound);
            }
            var votePerQuestion = await _context.VoteAnswers
                .Where(x=>x.Vote.PollId == pollId)
                .Select(x=> new VotesPerQuestionsResponse(
                    x.Question.Content,
                    x.Question.Votes
                    .GroupBy(x => new {AnsewrsId = x.Answer .Id ,AnswerContent = x.Answer.Content})
                    .Select(g=> new VotesPerAnswerResponse(
                        g.Key.AnswerContent,
                        g.Count()
                        ))
                    )).ToListAsync (cancellationToken);

            return Result.Succuss<IEnumerable<VotesPerQuestionsResponse>>(votePerQuestion);


        }
    }
}
