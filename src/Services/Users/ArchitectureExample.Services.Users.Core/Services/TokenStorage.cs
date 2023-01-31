using ArchitectureExample.Services.Users.Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Phaeton.Auth.JWT;
using Phaeton.DependencyInjection;

namespace ArchitectureExample.Services.Users.Core.Services;

[GenerateInterfaceAndRegisterIt(Lifetime.Singleton)]
public sealed partial class TokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpCtxAccessor;

    public TokenStorage(IHttpContextAccessor httpCtxAccessor)
        => _httpCtxAccessor = httpCtxAccessor;

    public void Set(JsonWebToken jwt)
        => _httpCtxAccessor.HttpContext?.Items.TryAdd(
            TokenKey,
            jwt
        );

    public JsonWebToken? Get()
    {
        if (_httpCtxAccessor.HttpContext is null)
            return null;

        if (_httpCtxAccessor.HttpContext.Items.TryGetValue(
            TokenKey,
            out var jwt
        ))
            return jwt as JsonWebToken;

        return null;
    }
}