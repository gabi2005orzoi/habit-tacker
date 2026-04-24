namespace backend.Exceptions;

public class UserNotFoundException: Exception
{
    public UserNotFoundException() : base("User not found") { }
    public UserNotFoundException(int userId) : base($"User with ID: {userId} not found"){}
    public UserNotFoundException(string message) : base(message){}
}