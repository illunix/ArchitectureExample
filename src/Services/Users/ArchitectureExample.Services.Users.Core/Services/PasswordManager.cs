using ArchitectureExample.Services.Users.Core.Services.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Phaeton.DependencyInjection;
using System.Security.Cryptography;

namespace ArchitectureExample.Services.Users.Core.Services;

[GenerateInterfaceAndRegisterIt(Lifetime.Singleton)]
internal sealed class PasswordManager : IPasswordManager
{
    public string Hash(
        string pwd,
        byte[] salt
    )
        => Hash(
            pwd,
            salt,
        KeyDerivationPrf.HMACSHA256
    );

    public byte[] GetRandomBytes()
    {
        var salt = new byte[32];
        RandomNumberGenerator
            .Create()
            .GetBytes(salt);

        return salt;
    }

    public bool IsValid(
        string pwd,
        string hashedPwd,
        string salt
    )
        => Hash(
            pwd,
            Convert.FromBase64String(salt)
        ) == hashedPwd;

    private static string Hash(
        string pwd,
        byte[] salt,
        KeyDerivationPrf prf
    )
        => Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: pwd,
                salt: salt,
                prf: prf,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            )
        );
}