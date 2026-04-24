using backend.Models.Dto;

namespace backend.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto> CreateUserAsync(UserCreateDto userDto);
    Task<bool> DeleteUser(int id);

    Task<UserDto?> UpdateUser(UserUpdateDto userDto);
}