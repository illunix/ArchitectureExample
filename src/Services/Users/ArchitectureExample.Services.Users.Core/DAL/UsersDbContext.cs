using ArchitectureExample.Services.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Phaeton.DAL.Postgres;

namespace ArchitectureExample.Services.Users.Core.DAL;

[DbContext]
public sealed partial class UsersDbContext
{
    public DbSet<UserEntity> Users { get; init; }
}