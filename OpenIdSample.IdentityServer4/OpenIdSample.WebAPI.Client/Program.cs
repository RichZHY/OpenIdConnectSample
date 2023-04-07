using OpenIdSample.WebAPI.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IIdentityServer4Service, IdentityServer4Service>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
