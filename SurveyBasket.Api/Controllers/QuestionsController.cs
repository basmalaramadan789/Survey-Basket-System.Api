using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.Contracts.Questions;


namespace SurveyBasket.Api.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, [FromQuery] RequestFilter filters, CancellationToken cancellationToken)
        {
            var result = await _questionService.GetAllAsync(pollId, filters, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        //Get By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int pollId, [FromRoute] int id , CancellationToken cancellationToken)
        {
            var result = await _questionService.GetAsync(pollId,id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromRoute]int pollId, [FromBody] QuestionsRequest request ,CancellationToken cancellationToken)
        {
            var result = await _questionService.AddAsync(pollId, request , cancellationToken);

            return result.IsSuccess ?
               CreatedAtAction(nameof(GetAsync), new { pollId, result.Value.Id }, result.Value) : result.ToProblem();



        }


        [HttpPut("")]
        public async Task<IActionResult> Update([FromRoute] int pollId, [FromRoute] int id, [FromBody] QuestionsRequest request, CancellationToken cancellationToken)
        {
            var result = await _questionService.UpdateAsync(pollId, id ,request, cancellationToken);
            return result.IsSuccess ?
                 NoContent() : result.ToProblem();
           

        }



        [HttpPut("{id}/ToggleStatus")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _questionService.ToggleStatusAsync(pollId,id, cancellationToken);
           
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
