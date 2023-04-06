using Microsoft.Extensions.Logging;
using Image_View_V1._0.Services;
using Image_View_V1._0.View;

namespace Image_View_V1._0;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ImageService>();

        builder.Services.AddSingleton<ImagesViewModel>();
        builder.Services.AddTransient<ImageDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailsPage>();

        return builder.Build();
    }
}

