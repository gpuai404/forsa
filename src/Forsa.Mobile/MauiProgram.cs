using Forsa.Mobile.ViewModels;
using Forsa.Mobile.Views;
using Microsoft.Extensions.Logging;
#if MAUI_DEVFLOW
using Microsoft.Maui.DevFlow.Agent;
#endif

namespace Forsa.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsFilled");
            fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIconsOutlined");
        });

    #if MAUI_DEVFLOW
        builder.AddMauiDevFlowAgent();
    #endif

        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddSingleton<Func<AppShell>>(services => () => services.GetRequiredService<AppShell>());

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
