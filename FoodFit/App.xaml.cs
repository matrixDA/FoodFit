using FoodFit.Views;

namespace FoodFit
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignUpPage());
        }
    }
}
