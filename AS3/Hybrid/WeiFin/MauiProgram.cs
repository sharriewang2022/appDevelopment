﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WeiFin.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WeiFin.Services;

namespace WeiFin;

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
#endif
        //import AntDesign
        builder.Services.AddAntDesign();

        builder.Services.AddSingleton<BillDBService>();
        builder.Services.AddSingleton<BillTypeDBService>();
        builder.Services.AddSingleton<UserRegisterDBService>();

        builder.Services.TryAddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();

        
        return builder.Build();
	}
}
