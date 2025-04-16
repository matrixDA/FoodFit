using FoodFit.Views;

namespace FoodFit
{
    public partial class App : Application
    {
            public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());


            MainPage = new NavigationPage(new LoginPage(new LocalDBService()));

        }

    }
}
