using ArchitectureExample.Shared.Core.Entities;

namespace ArchitectureExample.Services.Users.Core.Entities;

public sealed class UserEntity : EntityBase
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Salt { get; init; }
    public DateTime CreatedAt { get; } = DateTime.Now;
}