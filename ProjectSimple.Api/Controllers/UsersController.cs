using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectSimple.Application.Services.User.Commands.CreateUser;
using ProjectSimple.Application.Services.User.Commands.DeleteUser;
using ProjectSimple.Application.Services.User.Commands.UpdateUser;
using ProjectSimple.Application.Services.User.Queries.GetUserDetails;
using ProjectSimple.Application.Services.User.Queries.GetUsers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectSimple.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] bool? isActive)
        {
            var users = await _mediator.Send(new GetUsersQuery(isActive));

            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserDetailsDTO>> Get(long Id)
        {
            var user = await _mediator.Send(new GetUserDetailsQuery(Id));

            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> Post(CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);

            return CreatedAtAction(nameof(Get), new { Id = response });
        }

        // PUT api/<UsersController>
        [HttpPut]
        public async Task<ActionResult> Put(UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(long Id)
        {
            var command = new DeleteUserCommand() { Id = Id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}