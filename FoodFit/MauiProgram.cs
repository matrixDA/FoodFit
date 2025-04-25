using FoodFit.Models.ViewModels;
using FoodFit.Views;
using Microsoft.Extensions.Logging;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit
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
                });
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<SignUpPage>();
            builder.Services.AddSingleton<UserCreationPage>();
            builder.Services.AddTransient<SleepLogDetailsPage>();
            builder.Services.AddTransient<ProfilePage>();

            builder.Services.AddTransient<UpdateGoalPage>();
            builder.Services.AddTransient<UpdateWeight>();


            builder.Services.AddSingleton<LocalDBService>();
            builder.Services.AddTransient<FoodLogViewModel>();
            builder.Services.AddSingleton<userViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
