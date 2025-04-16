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
        var user = await _dbService.GetUserByUserName(userName.Text);

        var loginData = new Dictionary<string, object>()
        {
            {"userName", $"{user.UserName}" },
            {"userId", $"{user.UserId}" },
            {"userEmail", $"{user.Email}" },
        };
        if (!string.IsNullOrEmpty(userName.Text) | !string.IsNullOrEmpty(password.Text)) {
            if (user != null)
            {
                if (user.PasswordHash == password.Text) // Login Successful
                {
                    var appShell = (AppShell)Application.Current.MainPage;
                    appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
                    await Shell.Current.GoToAsync("//HomePage", loginData);
                }
                else if (string.IsNullOrEmpty(password.Text)) {
                    await DisplayAlert("Login Alert", "Username and/or Password cannot be empty", "OK");
                }
            }
            else
            {
                await DisplayAlert("Login Alert", "Username and/or Password cannot be empty", "OK");
            }
        }
        // If login is successful
       ;
	//	Navigation.PushAsync(new HomePage());
    }



    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());

    }
}//new test 