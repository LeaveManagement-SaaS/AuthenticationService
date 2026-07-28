using AuthenticationService.Application.Features.Users.Queries.GetAllUsers;
using AuthenticationService.Application.Features.Users.Queries.GetUserByEmail;
using AuthenticationService.Application.Features.Users.Queries.GetUserById;
using AuthenticationService.CrossCutting.Users.Commands.CreateUser;
using AuthenticationService.CrossCutting.Users.Commands.DeleteUser;
using AuthenticationService.CrossCutting.Users.Commands.UpdateUser;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using AuthenticationService.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // GET: api/User/email/test@test.com
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery(email));

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
      }

    // PUT: api/User/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and command id do not match.");

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return Ok();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));

        if (!result)
            return NotFound();

        return Ok();
    }
}