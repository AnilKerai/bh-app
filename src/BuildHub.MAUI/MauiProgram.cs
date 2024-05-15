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

		return builder.Build();
	}
}
