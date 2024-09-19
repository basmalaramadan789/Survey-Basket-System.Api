using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	[Authorize]
	[ApiVersion(1)]
	[ApiVersion(2)]
	public class PollsController : ControllerBase
	{
		private readonly IPollService _pollService;
		public PollsController(IPollService pollService)
		{
			_pollService = pollService;

		}

		[HttpGet("")]
					 
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
			var polls = await _pollService.GetAllAsync( cancellationToken);
			var response = polls.Adapt<IEnumerable<PollResponse>>();
			return Ok(response);
		}

        [HttpGet("Current")]
		[MapToApiVersion(1)]
        public async Task<IActionResult> GetCurrentv1(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetCurrentAsyncV1(cancellationToken));

        }

        [HttpGet("Current")]
        [MapToApiVersion(2)]
        public async Task<IActionResult> GetCurrentv2(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetCurrentAsyncV2(cancellationToken));

        }



        [HttpGet("{id}")]
		public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _pollService.GetAsync(id, cancellationToken);
			

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}
		[HttpPost("")]
		public async Task<IActionResult> Add([FromBody] CreatePollRequest reqeust,
			CancellationToken cancellationToken)
		{

			var result = await _pollService.AddAsync(reqeust, cancellationToken);

			return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();

           
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreatePollRequest reqeust,CancellationToken cancellationToken)
		{
			var result = await _pollService.UpdateAsync(id, reqeust,cancellationToken);
			
			return result.IsSuccess ? NoContent() : result.ToProblem();
				
				
        }

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _pollService.DeleteAsync(id, cancellationToken);
			
			return result.IsSuccess ?NoContent() : result.ToProblem();
        }

		[HttpPut("{id}/TogglePublish")]
		public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _pollService.TogglePublishStatusAsync(id, cancellationToken);
           
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }



	}
}