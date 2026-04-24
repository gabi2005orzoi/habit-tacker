using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = null!;
    
    public ICollection<Habit> Habits { get; set; } = null!;
}