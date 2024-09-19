using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    public class ResultsController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        [HttpGet("row-data")]
        public async Task<IActionResult> PollVotes([FromRoute] int pollId,CancellationToken cancellationToken)
        {
            var result = await _resultService.GetPollVotesAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("vote-per-day")]
        public async Task<IActionResult> VotesPerDay([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetVotesPerDayAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }


        [HttpGet("vote-per-question")]
        public async Task<IActionResult> VotesPerquestion([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _resultService.GetVotesPerQuestionsAsync(pollId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

    }
}
