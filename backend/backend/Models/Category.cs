using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;
    public List<Habit> Habits { get; set; } = null!;
}