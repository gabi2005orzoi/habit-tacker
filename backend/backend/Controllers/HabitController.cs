using backend.Models.Dto.HabitsDto;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Controller]
[Route("/api/[controller]")]
public class HabitController(HabitService habitService): ControllerBase
{
    private readonly HabitService _habitService = habitService;

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(int userId)
    {
        return Ok(await _habitService.GetAllByUserId(userId));
    }

    [HttpGet("{userId}/{habitId}")]
    public async Task<IActionResult> GetAll(int userId, int habitId)
    {
        return Ok(await _habitService.GetById(userId, habitId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabit(CreateHabitDto dto)
    {
        return Ok(await _habitService.CreateHabit(dto));
    }

    [HttpDelete("{id}")]
    public async Task DeleteHabit(int id)
    {
        await _habitService.DeleteHabit(id);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateHabit(UpdateHabitDto dto)
    {
        return Ok(await _habitService.UpdateHabit(dto));
    }

    [HttpPut("toggle")]
    public async Task ToggleHabit(ToggleHabitDto dto)
    {
        await _habitService.ToggleHabit(dto);
    }
}