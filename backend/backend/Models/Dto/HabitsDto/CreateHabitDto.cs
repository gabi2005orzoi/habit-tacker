namespace backend.Models.Dto.HabitsDto;

public record CreateHabitDto(string Name, TimeSpan Duration, int UserId, int? CategoryId);