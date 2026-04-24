using backend.Data;
using backend.Exceptions;
using backend.Models;
using backend.Models.Dto;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UserService(AppDbContext context): IUserService
{
    private readonly AppDbContext _context = context;


    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users.Select(u => new UserDto(u.Id, u.Username, u.Email)).ToListAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new UserNotFoundException(); 
        return new UserDto(user.Id, user.Username, user.Email);
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            PasswordHash = userDto.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserDto(user.Id, user.Username, user.Email);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;
        _context.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto?> UpdateUser(UserUpdateDto userDto)
    {
        var user = await _context.Users.FindAsync(userDto.Id);
        if (user == null)
            throw new UserNotFoundException(); 
        user.Username = userDto.Username;
        user.Email = userDto.Email;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new UserDto(user.Id, user.Username, user.Email);
    }
}