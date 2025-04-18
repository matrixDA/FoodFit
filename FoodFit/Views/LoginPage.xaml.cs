using System.Diagnostics;
using FoodFit.Models;

namespace FoodFit.Views;

public partial class LoginPage : ContentPage
{
    private readonly LocalDBService _dbService;
	public LoginPage(LocalDBService dBService)
	{
		InitializeComponent();
        _dbService = dBService;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(password.Text))
        {
            var user = await _dbService.GetUserByUserName(userName.Text, password.Text);

            if (user != null)
            {
                var loginData = new Dictionary<string, object>()
                {
                    {"userName", $"{user.UserName}" },
                    {"userId", $"{user.UserId}" },
                    {"userEmail", $"{user.Email}" },
                };

                var appShell = (AppShell)Application.Current.MainPage;
                appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
                await Shell.Current.GoToAsync("//HomePage", loginData);
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