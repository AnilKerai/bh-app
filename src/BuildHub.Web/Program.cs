using Auth0.OidcClient;
using BuildHub.Shared.Interfaces;
using BuildHub.Web.Auth0;
using BuildHub.Web.Components;
using BuildHub.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder
    .Services
        .AddScoped<IFormFactor, FormFactor>()
        .AddSingleton(new Auth0Client(new Auth0ClientOptions
        {
            Domain = "dev-j7idk0ol3eitivex.us.auth0.com",
            ClientId = "P5M7RkGGi9jB2qKKv4LVCMbZTtofRhp3",
            RedirectUri = "myapp://callback",
            PostLogoutRedirectUri = "myapp://callback",
            Scope = "openid profile email"
        }))
        .AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(BuildHub.Shared._Imports).Assembly);

app.Run();