using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WeFin.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WeFin.Services;

namespace WeFin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
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
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif
        //import AntDesign
        builder.Services.AddAntDesign();

        builder.Services.AddSingleton<BillDBService>();
        builder.Services.AddSingleton<BillTypeDBService>();
        builder.Services.AddSingleton<UserLoginDBService>();

        builder.Services.TryAddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();

        //Register needed elements for authentication
        builder.Services.AddAuthorizationCore(); // This is the core functionality
        // This is the custom provider
        builder.Services.AddScoped<CustomAuthenticationStateProvider>(); 
         //When asking for the default Microsoft one, give these
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

        return builder.Build();
	}
}
