using Phaeton.Abstractions;

namespace ArchitectureExample.Services.Users.Core.Exceptions;

internal class EmailInUseException : ExceptionBase
{
    public string Email { get; }

    public EmailInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}