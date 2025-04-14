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

       Debug.WriteLine("hello world");
        var user = await _dbService.GetUserByUserName(userName.Text);
        if (user != null)
        {
           
        }
 
        // If login is successful
        var appShell = (AppShell)Application.Current.MainPage;
        appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
        await Shell.Current.GoToAsync("//HomePage");
	//	Navigation.PushAsync(new HomePage());

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());

    }
}//new test 