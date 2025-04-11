
using FoodFit.Models;
namespace FoodFit.Views;

public partial class UserCreationPage : ContentPage
{
    string username;
    string email;
    string password;
    private readonly LocalDBService _dbService;

    public UserCreationPage(string username, string email, string password, LocalDBService dBService)
	{
		InitializeComponent();
        this.username = username;
        this.email = email;
        this.password = password;
        _dbService = dBService;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _dbService.CreateUser(new User
        {
            UserName = username,
            Email = email,
            PasswordHash = password,
            CurrentWeight = Convert.ToDouble(currentWeight.Text),
            GoalWeight = Convert.ToDouble(goalWeight.Text),
            height = Convert.ToDouble(height.Text)
        });
        Navigation.PopToRootAsync();
    }
}