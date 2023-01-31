using Phaeton.Abstractions;

namespace ArchitectureExample.Services.Users.Core.Exceptions;

internal class InvalidCredentialsException : ExceptionBase
{
    public InvalidCredentialsException() : base("Invalid credentials.") { }
}