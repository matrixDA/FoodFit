namespace FoodFit.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}


    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Perform login logic here

        // If login is successful
        var appShell = (AppShell)Application.Current.MainPage;
        appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
        await Shell.Current.GoToAsync("//HomePage");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());

    }
}