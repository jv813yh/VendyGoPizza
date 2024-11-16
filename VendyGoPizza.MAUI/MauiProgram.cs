
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace VendyGoPizza.MAUI
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            // Register services
            builder.Services.AddSingleton<PizzaService>();

            // Register HomePage and HomeViewModel with ShellRoute
            builder.Services.AddSingletonWithShellRoute<HomePage, HomeViewModel>(nameof(HomePage));
            // Register AllPizzasPage and AllPizzasViewModel with ShellRoute
            builder.Services.AddSingletonWithShellRoute<AllPizzasPage, AllPizzasViewModel>(nameof(AllPizzasPage));
            // Register DetailsPage and DetailsPageViewModel with ShellRoute
            builder.Services.AddSingletonWithShellRoute<DetailsPage, DetailsPageViewModel>(nameof(DetailsPage));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
