using Microsoft.Extensions.Logging;
using MauiBlazorApp.Data;

namespace MauiBlazorApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            //.RegisterBlazorMauiWebView()
            //.UseMicrosoftExtensionsServiceProviderFactory()
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

		builder.Services.AddSingleton<WeatherForecastService>();

        //import AntDesign
        builder.Services.AddAntDesign();
        //set parameter
        /* builder.Services.Configure<ProSettings>(settings =>
         {
             settings.NavTheme = "light";
             settings.Layout = "side";
             settings.ContentWidth = "Fluid";
             settings.FixedHeader = false;
             settings.FixSiderbar = true;
             settings.Title = "Bank Balance App";
             settings.PrimaryColor = "daybreak";
             settings.ColorWeak = false;
             settings.SplitMenus = false;
             settings.HeaderRender = true;
             settings.FooterRender = false;
             settings.MenuRender = true;
             settings.MenuHeaderRender = true;
             settings.HeaderHeight = 48;

         });*/

        //builder.Services.AddSingleton<BillDBService>(s => ActivatorUtilities.CreateInstance<BillDBService>(s, dbPath));
        builder.Services.AddSingleton<BillDBService>();
        builder.Services.AddSingleton<BillTypeDBService>();
        //builder.Services.AddSingleton(typeof(IDialogService), typeof(DialogService));
        return builder.Build();
	}
}
