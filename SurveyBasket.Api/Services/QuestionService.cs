using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Answers;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.Contracts.Questions;
using SurveyBasket.Api.Errors;


namespace SurveyBasket.Api.Services
{
    public class QuestionService(ApplicationDbContext context,ICacheService cacheService) : IQuestionService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ICacheService _cacheService = cacheService;

        private const string _cachePrefix = "availableQuestion";


        public async Task<Result<PaginatedList<QuestionosResponse>>> GetAllAsync(int pollId,RequestFilter filters, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.polls.AnyAsync(x => x.Id == pollId, cancellationToken: cancellationToken);
            if (!pollIsExist)
            {
                return Result.Failure <PaginatedList<QuestionosResponse>>(PollEerrors.pollNotFound);


            }
            var query =  _context.Questions
                .Where(x => x.PollId == pollId && (string.IsNullOrEmpty(filters.SearchValue) || x.Content.Contains(filters.SearchValue)))
                .Include(x => x.Answers)
                
                .ProjectToType<QuestionosResponse>()
                .AsNoTracking();
            var questions = await PaginatedList<QuestionosResponse>.CreateAsync(query,filters.PageNumber,filters.PageSize,  cancellationToken);
                

            return Result.Succuss(questions);
        }



        public async Task<Result<IEnumerable<QuestionosResponse>>> GetAvailabeAsync(int pollId, string userId, CancellationToken cancellationToken = default)
        {
            

            var cacheKey = $"{_cachePrefix}-{pollId}";

            var cachedQuestion= await _cacheService.GetAsunc<IEnumerable<QuestionosResponse>>(cacheKey, cancellationToken);
            IEnumerable<QuestionosResponse> questions = [];

            if(cachedQuestion is null)
            {
                questions=await _context.Questions
                .Where(x => x.PollId == pollId && x.IsActive)
                .Include(x => x.Answers)
                .Select(q => new QuestionosResponse(
                    q.Id,
                    q.Content,
                    q.Answers.Where(a => a.IsActive).Select(a => new AnswerResponse(a.Id, a.Content))

                )).AsNoTracking()
                        .ToListAsync(cancellationToken: cancellationToken);

                await _cacheService.SetAsunc(cacheKey, questions, cancellationToken);


            }
            else
            {
                questions = cachedQuestion;
            }

            

            return Result.Succuss<IEnumerable<QuestionosResponse>>(questions!);
        }

        //get by id
        public async Task<Result<QuestionosResponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
           var question = await _context.Questions
                .Where(x => x.PollId == pollId && x.Id ==id)
                .Include(x => x.Answers)
                .ProjectToType<QuestionosResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if(question == null)
            {
                return Result.Failure<QuestionosResponse>(QuestionErrors.questionNotFound);

            }
            return Result.Succuss(question);
        }


        public async Task<Result<QuestionosResponse>> AddAsync(int pollId, QuestionsRequest request, CancellationToken cancellationToken = default)
        {
            //check is there poll to put question
            var pollIsExist = await _context.polls.AnyAsync(x =>x.Id== pollId, cancellationToken: cancellationToken); 
            if (!pollIsExist)
            {
                return Result.Failure<QuestionosResponse>(PollEerrors.pollNotFound);
            }
            // ensure that the poll doesnt have question with the same content
            var questionIsExit = await _context.Questions.AnyAsync(x => x.Content == request.Content && x.PollId == pollId, cancellationToken: cancellationToken);
            if (questionIsExit) 
                {
                    return Result.Failure<QuestionosResponse>(QuestionErrors.DuplicatedQustion);

                }
            var question = request.Adapt<Question>();
            question.PollId = pollId;

            request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));
            await _context.AddAsync(question, cancellationToken); 
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsunc($"{_cachePrefix}-{pollId}",cancellationToken);

            return Result.Succuss(question.Adapt<QuestionosResponse>());
        }

        public async Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(x => x.PollId==pollId && x.Id==id, cancellationToken: cancellationToken);
            if (question is null)
            {
                return Result.Failure(PollEerrors.pollNotFound);
            }

            question.IsActive = !question.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsunc($"{_cachePrefix}-{pollId}",cancellationToken);

            return Result.Success();
        }

        public async Task<Result> UpdateAsync(int pollId, int id, QuestionsRequest request, CancellationToken cancellationToken = default)
        {
            var questionIsExist = await _context.Questions.AnyAsync(x=>x.PollId== pollId && x.Id!=id && x.Content ==request.Content,  cancellationToken);
            if (questionIsExist)
            {
                return Result.Failure(QuestionErrors.DuplicatedQustion);
            }
            var question = await _context.Questions
                .Include(x => x.Answers)
                .SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken);
            if(question is null)
            {
                return Result.Failure(QuestionErrors.questionNotFound);
            }
            //current answer
            var currentAnswer = question .Answers .Select(x=>x.Content).ToList();
            //Add new answers
            var newAnswer = request .Answers .Except(currentAnswer).ToList();
            newAnswer.ForEach(answer => {
                question.Answers.Add(new Answer { Content = answer });
            });
            question.Answers.ToList().ForEach(answer => { 
                answer.IsActive= request.Answers.Contains(answer.Content);

            });
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveAsunc($"{_cachePrefix}-{pollId}",cancellationToken);
            return Result.Success();
        }
    }
}
