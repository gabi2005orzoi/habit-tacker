using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Habit> Habits { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<HabitLog> HabitLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User-Habit <=> one-to-many
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Habit-HabitLog <=> one-to-many
        modelBuilder.Entity<HabitLog>()
            .HasOne(hl => hl.Habit)
            .WithMany(h => h.HabitLogs)
            .HasForeignKey(h => h.HabitId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Category-Habit <=> one-to-many
        modelBuilder.Entity<Habit>()
            .HasOne(h => h.Category)
            .WithMany(c => c.Habits)
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // A habit can be completed only once a day
        modelBuilder.Entity<HabitLog>()
            .HasIndex(hl => new { hl.HabitId, hl.Date })
            .IsUnique();
    }
}