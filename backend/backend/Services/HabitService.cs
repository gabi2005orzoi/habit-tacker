using backend.Data;
using backend.Exceptions;
using backend.Models;
using backend.Models.Dto.HabitsDto;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class HabitService(AppDbContext context): IHabitService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Habit>> GetAllByUserId(int userId)
    {
        var exists = await _context.Users.AnyAsync(u => u.Id == userId);
        
        if (!exists)
        {
            throw new Exception("The user doesn't exist");
        }

        return await _context.Habits.Where(h => h.UserId == userId).ToListAsync();
    }

    public async Task<Habit> GetById(int userId, int habitId)
    {
        var user = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!user)
        {
            throw new UserNotFoundException();
        }
        
        var habit = await _context.Habits.FindAsync(habitId);

        if (habit == null)
        {
            throw new HabitNotFoundException();
        }

        return habit;
    }
    
    public async Task<Habit> CreateHabit(CreateHabitDto dto)
    {
        var user = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!user)
        {
            throw new Exception("The user doesn't exist");
        }
        
        var habit = new Habit
        {
            Name = dto.Name,
            Duration = dto.Duration,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId
        };

        await _context.AddAsync(habit);
        await _context.SaveChangesAsync();

        return habit;
    }

    public async Task DeleteHabit(int habitId)
    {
        var habit = await _context.Habits.FindAsync(habitId);

        if (habit == null)
        {
            throw new HabitNotFoundException();
        }

        _context.Remove(habit);
        await _context.SaveChangesAsync();
    }

    public async Task<Habit> UpdateHabit(UpdateHabitDto dto)
    {
        var habit = await _context.Habits.FindAsync(dto.Id);

        if (habit == null)
        {
            throw new HabitNotFoundException();
        }

        if (dto.CategoryId != null)
            habit.CategoryId = dto.CategoryId;
        if (dto.Name != null)
            habit.Name = dto.Name;
        if (dto.Duration != null)
            habit.Duration = dto.Duration.Value;

        _context.Update(habit);
        await _context.SaveChangesAsync();

        return habit;
    }

    public async Task ToggleHabit(ToggleHabitDto dto)
    {

        var existingLog = await _context.HabitLogs.FirstOrDefaultAsync(hl => hl.HabitId == dto.HabitId && hl.Date == dto.Date);

        if (existingLog != null)
            existingLog.Completed = !existingLog.Completed;
        else
        {
            _context.HabitLogs.Add(new HabitLog
            {
                HabitId = dto.HabitId,
                Date = dto.Date,
                Completed = true
            });
        }

        await _context.SaveChangesAsync();
    }
}