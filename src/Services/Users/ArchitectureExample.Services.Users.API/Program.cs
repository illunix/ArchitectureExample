using ArchitectureExample.Services.Users.Core.Features;
using ArchitectureExample.Services.Users.Core.Services.Abstractions;
using ArchitectureExample.Services.Users.Core;
using Phaeton.DependencyInjection;
using Phaeton.Abstractions;
using Phaeton.Framework;

var builder = WebApplication
    .CreateBuilder(args)
    .AddPhaetonFramework();

builder.Services.AddCore(builder.Configuration).RegisterServicesFromAssembly();

var app = builder.Build();

app.MapPost(
    "/api/sign-in",
    async (
        SignIn.Command req,
        IMediator mediator,
        ITokenStorage storage
    ) =>
    {
        await mediator.Send(req);

        return Results.Ok(storage.Get());
    }
)
    .WithTags("Account")
    .WithName("Sign In")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);

app.MapPost(
    "/api/sign-up",
    async (
        SignUp.Command req,
        IMediator mediator
    ) =>
    {
        await mediator.Send(req);

        return Results.NoContent();
    }
)
    .WithTags("Account")
    .WithName("Sign up")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest);

app.UsePhaetonFramework();
app.UseHttpsRedirection();
app.Run();