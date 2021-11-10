using System;

namespace WebAPI.exceptions
{
    public static class Status
    {
        public const string UserNotFound = "User Not Found";
        public const string UserAlreadyExists = "User Already Exists";
        public const string IncorrectPassword = "Incorrect Password";
    }
}