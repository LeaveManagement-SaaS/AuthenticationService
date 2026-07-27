using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // GET: api/User/email/test@test.com
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        await _userRepository.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // PUT: api/User/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
}