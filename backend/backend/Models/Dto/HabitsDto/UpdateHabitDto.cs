namespace backend.Models.Dto.HabitsDto;

public record UpdateHabitDto(int Id, string? Name, TimeSpan? Duration, int?CategoryId);