/**
* @file MauiProgram.cs
* @brief Contains the MauiProgram class, which is the entry point of the application.
*/

using Microsoft.Extensions.Logging;
using Image_View_V1._0.Services;
using Image_View_V1._0.Helpers;
using Image_View_V1._0.View;
using Windows.UI.ViewManagement;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;

namespace Image_View_V1._0;

/**
* @brief The MauiProgram class, which is the entry point of the application.
*/

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureMopups() 
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        
        builder.Services.AddSingleton<ImageService>();
        builder.Services.AddSingleton<ImageDataBaseService>();
        builder.Services.AddSingleton<ImageToProcessHelper>();

        builder.Services.AddSingleton<ImagesViewModel>();
        builder.Services.AddTransient<ImageDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailsPage>();
        builder.Services
            .AddTransient<PopUpWithLoadDataFromDataBase>();

        builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        builder.Services.AddTransient<DetailsPage>();

        var app = builder.Build();

        // Konfiguracja rozmiaru okna dla platformy Windows
#if WINDOWS
        if (app.Services.GetService<Microsoft.UI.Xaml.Window>() is Microsoft.UI.Xaml.Window mainWindow)
        {
            mainWindow.Activated += (sender, args) =>
            {
                var applicationView = ApplicationView.GetForCurrentView();
                applicationView.SetPreferredMinSize(new Windows.Foundation.Size(1224, 982));
                ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(1224, 982);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            };
        }
#endif

        return app;
    }
}

