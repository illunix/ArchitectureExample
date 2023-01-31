using ArchitectureExample.Services.Users.Core.DAL.Abstractions;
using ArchitectureExample.Services.Users.Core.Entities;
using ArchitectureExample.Services.Users.Core.Exceptions;
using ArchitectureExample.Services.Users.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Phaeton.Abstractions;
using Phaeton.Mediator;
using Phaeton.Utilities;

namespace ArchitectureExample.Services.Users.Core.Features;

[GenerateMediator]
public sealed partial class SignUp
{
    public sealed partial record Command(
        string Email,
        string Password,
        string ConfirmPassword
    )
    {
        public static void Validate(Command req)
        {
            ArgumentException.ThrowIfNullOrEmpty(
                req.Email,
                nameof(req.Email)
            );
            if (!RegexUtilities.IsValidEmail(req.Email))
                throw new ArgumentException(
                    "Email is invalid.",
                    nameof(req.Email)
                );
            ArgumentException.ThrowIfNullOrEmpty(
                req.Password,
                nameof(req.Password)
            );
            ArgumentException.ThrowIfNullOrEmpty(
                req.ConfirmPassword,
                nameof(req.ConfirmPassword)
            );
            if (req.ConfirmPassword != req.Password)
                throw new ArgumentException(
                    "Invalid confirm password, passwords are different.",
                    nameof(req.ConfirmPassword)
                );
        }
    }

    public static async Task Handler(
        Command req,
        IPasswordManager pwdMgr,
        IIdGen idGen,
        IUsersDbContext ctx
    )
    {
        if (await ctx.Users
            .AnyAsync(q => q.Email == req.Email)
            .ConfigureAwait(false)
        )
            throw new EmailInUseException(req.Email);

        var salt = pwdMgr.GetRandomBytes();
        var user = new UserEntity
        {
            Id = idGen.Create(),
            Email = req.Email,
            Password = pwdMgr.Hash(
                req.Password,
                salt
            ),
            Salt = Convert.ToBase64String(salt),
        };

        ctx.Users.Add(user);

        await ctx
            .SaveChanges()
            .ConfigureAwait(false);
    }
}
