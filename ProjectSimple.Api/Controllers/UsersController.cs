﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectSimple.Application.Services.User.Commands.CreateUser;
using ProjectSimple.Application.Services.User.Commands.DeleteUser;
using ProjectSimple.Application.Services.User.Commands.GetUsers;
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

        // GET api/<UsersController>/5
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetailsDTO>> Get(long Id)
        {
            var user = await _mediator.Send(new GetUserDetailsQuery(Id));

            return Ok(user);
        }

        // GET: api/<UsersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("WebReturn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUsersCommandResponse>> Post(GetUsersCommand getUsersCommand)
        {
            var users = await _mediator.Send(getUsersCommand);

            return Ok(users);
        }


        // POST api/<UsersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);

            return CreatedAtAction(nameof(Get), new { Id = response });
        }

        // PUT api/<UsersController>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(long Id)
        {
            var command = new DeleteUserCommand() { Id = Id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}