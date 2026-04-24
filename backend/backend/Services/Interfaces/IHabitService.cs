using backend.Models;
using backend.Models.Dto.HabitsDto;

namespace backend.Services.Interfaces;

public interface IHabitService
{
    Task<IEnumerable<Habit>> GetAllByUserId(int userId);
    Task<Habit> GetById(int userId, int habitId);
    Task<Habit> CreateHabit(CreateHabitDto dto);
    Task DeleteHabit(int habitId);
    Task<Habit> UpdateHabit(UpdateHabitDto dto);
    Task ToggleHabit(ToggleHabitDto dto);
}