namespace FoodFit.Views;
using FoodFit.Models;
using FoodFit.ViewModels;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(userViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
    
}