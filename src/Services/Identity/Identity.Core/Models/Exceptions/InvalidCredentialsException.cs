using System;

namespace Identity.Core.Models.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid username or password")
        {
        }
    }
}
