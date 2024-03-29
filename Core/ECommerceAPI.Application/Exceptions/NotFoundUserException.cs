﻿namespace ECommerceAPI.Application.Exceptions;
public class NotFoundUserException : Exception {
    public NotFoundUserException() : base("Username & email or password wrong") { }

    public NotFoundUserException(String? message) : base(message) { }

    public NotFoundUserException(String? message, Exception? innerException) : base(message, innerException) { }
}
public class PasswordChangeFailedException : Exception {
    public PasswordChangeFailedException() : base("Could not update password") { }

    public PasswordChangeFailedException(String? message) : base(message) { }

    public PasswordChangeFailedException(String? message, Exception? innerException) : base(message, innerException) { }
}