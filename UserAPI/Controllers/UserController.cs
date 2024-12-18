using Microsoft.AspNetCore.Mvc;
using UserAPI.Application;

namespace UserAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] User request)
    {
        try
        {
            var user = _userService.CreateUser(request.FirstName, request.LastName, request.Email);
          //  return CreatedAtActionResult(user.Id);
          return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody] User request)
    {
        try
        {
            _userService.UpdateUser(request.Id, request.FirstName, request.LastName, request.Email);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        try
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
