using CleanArchitecture.Application.Resources.UserResources.CreateUser;
using CleanArchitecture.Application.Resources.UserResources.GetAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet("")]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<ActionResult<CreateUserResponse>> PostAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
