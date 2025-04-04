namespace FoodFit.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}
    private async void OnSignupClicked(object sender, EventArgs e)
    {
        string username = EntryUsername.Text;
        string email = EntryEmail.Text;
        string password = EntryPassword.Text;
        string confirmPassword = EntryConfirmPassword.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill out all fields.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        await DisplayAlert("Success", "Account created successfully!", "OK");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
       Navigation.PushAsync(new LoginPage());
    }
}