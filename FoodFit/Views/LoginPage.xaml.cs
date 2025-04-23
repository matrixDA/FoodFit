using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class LoginPage : ContentPage
{
    private readonly userViewModel _userViewModel;
    private readonly LocalDBService _dbService;
	public LoginPage(userViewModel userViewModel,LocalDBService dBService)
	{
		InitializeComponent();
        _userViewModel = userViewModel;
        _dbService = dBService;
        BindingContext = _userViewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(password.Text))
        {
            var user = await _dbService.GetUserByUserName(userName.Text, password.Text);

            if (user != null)
            {
                _userViewModel.UserName = user.UserName;
                _userViewModel.UserEmail = user.Email;
                _userViewModel.UserId = user.UserId;

                await _dbService.AddFoodItem(new Foods
                {
                    FoodName = "Apple",
                    Calories = 20,
                    Carbs = 20,
                    Protein = 0,
                    Fat = 5,
                    Unit = "single"
                });
                await _dbService.AddFoodItem(new Foods
                {
                    FoodName = "Banana",
                    Calories = 20,
                    Carbs = 20,
                    Protein = 0,
                    Fat = 5,
                    Unit = "single"
                });

                //var loginData = new Dictionary<string, object>()
                //{
                //    {"userName", $"{user.UserName}" },
                //    {"userId", $"{user.UserId}" },
                //    {"userEmail", $"{user.Email}" },
                //};
                var appShell = (AppShell)Application.Current.MainPage;
                appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
                await Shell.Current.GoToAsync($"//HomePage");

            }
            else { await DisplayAlert("Login Error", "Username or Password incorrect", "OK"); }
        }
        else
        {
           await DisplayAlert("Login Error", "Username or Password cannot be blank", "OK");
        }
    }
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());

    }
}//new test 