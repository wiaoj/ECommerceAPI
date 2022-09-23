namespace ECommerceAPI.Application.Exceptions;
public class UserCreateFailedException : Exception {
    public UserCreateFailedException() : base("An unexpected error was encountered while creating a user.") { }

    public UserCreateFailedException(String? message) : base(message) { }

    public UserCreateFailedException(String? message, Exception? innerException) : base(message, innerException) { }
}