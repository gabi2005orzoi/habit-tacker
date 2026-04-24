using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Habit
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public TimeSpan Duration { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;

    public ICollection<HabitLog> HabitLogs { get; set; } = null!;
    
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

}