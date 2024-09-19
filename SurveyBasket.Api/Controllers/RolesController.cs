using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Roles;

namespace SurveyBasket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet("")]
 
        public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled)
        {
            var roles = await _roleService.GetAllAsync(includeDisabled);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get ([FromRoute] string id)
        {
            var result = await _roleService.GetAsync(id);

            return result.IsSuccess? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] RoleRequest request)
        {
            var result = await _roleService.AddAsync(request);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id,[FromBody] RoleRequest request)
        {
            var result = await _roleService.UpdateAsync(id,request);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/toggle-status")]
       
        public async Task<IActionResult> ToggleStatus([FromRoute] string id)
        {
            var result = await _roleService.ToggleStatusAsync(id);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
