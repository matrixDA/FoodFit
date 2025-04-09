using Microsoft.Maui.Controls;
using FoodFit.ViewModels;

namespace FoodFit.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
    }
}
