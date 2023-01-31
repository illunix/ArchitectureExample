using ArchitectureExample.Services.Users.Core.DAL.Abstractions;
using ArchitectureExample.Services.Users.Core.Exceptions;
using ArchitectureExample.Services.Users.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Phaeton.Auth.JWT.Abstractions;
using Phaeton.Mediator;
using Phaeton.Utilities;

namespace ArchitectureExample.Services.Users.Core.Features;

[GenerateMediator]
public sealed partial class SignIn
{
    public sealed partial record Command(
        string Email,
        string Password
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
                    "Invalid email.",
                    nameof(req.Email)
                );
            ArgumentException.ThrowIfNullOrEmpty(
                req.Password,
                nameof(req.Password)
            );
        }
    }

    public static async Task Handler(
        Command req,
        IPasswordManager pwdMgr,
        ITokenStorage tokenStorage,
        IUsersDbContext ctx,
        IJsonWebTokenManager jwtMgr
    )
    {
        var user = await ctx.Users.FirstOrDefaultAsync(q => q.Email == req.Email);
        if (
            user is null ||
            !pwdMgr.IsValid(
                req.Password,
                user.Password,
                user.Salt
            )
        )
            throw new InvalidCredentialsException();

        tokenStorage.Set(jwtMgr.CreateToken(
            user.Id,
            user.Email
        ));
    }
}