﻿using SurveyBasket.Api.Contracts.Votes;
using SurveyBasket.Api.Errors;

namespace SurveyBasket.Api.Services
{
    public class VoteService(ApplicationDbContext context) : IVoteService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddVoteAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default)
        {
            var hasVote = await _context.Votes.AnyAsync(x => x.PollId == pollId && x.UserId == userId, cancellationToken: cancellationToken);
            if (hasVote)
            {
                return Result.Failure(VoteEerror.DuplicatedVote);
            }
            var pollIsExist = await _context.polls.AnyAsync(x => x.Id == pollId && x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken: cancellationToken);
            if (!pollIsExist)
            {
                return Result.Failure(PollEerrors.pollNotFound);
            }

            var availableQuestions = await _context.Questions
                .Where(x=>x.PollId == pollId && x.IsActive)
                .Select(x=>x.Id)
                .ToListAsync(cancellationToken: cancellationToken);
            if (!request.Answers.Select(x => x.QuestionId).SequenceEqual(availableQuestions))
            {
                return Result.Failure(VoteEerror.InvalidQuestions);
            }
            var vote = new Vote
            {
                PollId= pollId,
                UserId= userId,
                VoteAnswers=request.Answers.Adapt<IEnumerable<VoteAnswer>>().ToList()
            };
            await _context.AddAsync(vote,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
