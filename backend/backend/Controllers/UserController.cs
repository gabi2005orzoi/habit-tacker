using backend.Models.Dto;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateDto userDto)
    {
        return Ok(await _userService.CreateUserAsync(userDto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return Ok(await _userService.DeleteUser(id));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userDto)
    {
        return Ok(await _userService.UpdateUser(userDto));
    }
}