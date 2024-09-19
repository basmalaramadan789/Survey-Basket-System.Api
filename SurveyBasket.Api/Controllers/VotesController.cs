﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Abstractions.Consts;
using SurveyBasket.Api.Contracts.Votes;


namespace SurveyBasket.Api.Controllers
{
    [Route("api/polls/{PollId}/Vote")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Member)]
    [EnableRateLimiting("Concurrency")]
    public class VotesController(IQuestionService questionService, IVoteService voteService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;
        private readonly IVoteService _voteService = voteService;

        [HttpGet("")]
       
        public async Task<IActionResult> Start([FromRoute] int pollId , CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var result = await _questionService.GetAvailabeAsync(pollId, userId!, cancellationToken);

           return result.IsSuccess ?
                 Ok(result.Value): result.ToProblem();
            
           

        }

        [HttpPost("")]
        public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody] VoteRequest request , CancellationToken cancellationToken)
        {
            var result = await _voteService.AddVoteAsync(pollId ,User.GetUserId()!, request , cancellationToken);

           return result.IsSuccess ? Created() : result.ToProblem();


        }

    }


}
