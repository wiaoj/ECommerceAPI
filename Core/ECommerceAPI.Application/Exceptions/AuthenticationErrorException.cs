﻿namespace ECommerceAPI.Application.Exceptions;
public class AuthenticationErrorException : Exception {
    public AuthenticationErrorException() : base("Authentication exception") { }

    public AuthenticationErrorException(String? message) : base(message) { }

    public AuthenticationErrorException(String? message, Exception? innerException) : base(message, innerException) { }
}