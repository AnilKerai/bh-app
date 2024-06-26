using Auth0.OidcClient;
using BuildHub.MAUI.Services;
using BuildHub.Shared;
using BuildHub.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace BuildHub.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();
		
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IFormFactor, FormFactor>();
		builder.Services.AddSingleton(new Auth0Client(new Auth0ClientOptions
		{
			Domain = "dev-j7idk0ol3eitivex.us.auth0.com",
			ClientId = "P5M7RkGGi9jB2qKKv4LVCMbZTtofRhp3",
			RedirectUri = "myapp://callback",
			PostLogoutRedirectUri = "myapp://callback",
			Scope = "openid profile email"
		}));

		return builder.Build();
	}
}
